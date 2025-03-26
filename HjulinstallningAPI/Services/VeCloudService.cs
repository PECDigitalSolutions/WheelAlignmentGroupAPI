using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;
using ServiceReference; // ✅ Ensure correct namespace

namespace HjulinstallningAPI.Services
{
    public class VeCloudService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _idKey;
        private readonly string _kundId;
        private readonly string _lk;
        private readonly string _password;
        private readonly int _produktId;

        public VeCloudService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = "https://ais-vecloud.azurewebsites.net/VeCloudService.svc"; // 🔹 Hardcoded from DNB Docs

            _idKey = configuration["DnbApi:IdKey"] ?? throw new InvalidOperationException("IdKey is missing");
            _kundId = configuration["DnbApi:KundId"] ?? throw new InvalidOperationException("KundId is missing");
            _lk = configuration["DnbApi:Lk"] ?? throw new InvalidOperationException("Lk is missing");
            _password = configuration["DnbApi:Password"] ?? throw new InvalidOperationException("Password is missing");
            _produktId = int.Parse(configuration["DnbApi:ProduktId"] ?? throw new InvalidOperationException("ProduktId is missing"));
        }

        public async Task<string> GetVehicleDataAsync(string licensePlate)
        {
            string hashedPassword = HashPassword(licensePlate, _password);

            // ✅ **Exact XML format from DNB Documentation**
            var soapRequest = $@"
            <soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/'
                              xmlns:tem='http://tempuri.org/'
                              xmlns:vec='http://schemas.datacontract.org/2004/07/VeCloud'>
                <soapenv:Header/>
                <soapenv:Body>
                    <tem:GetRegnr>
                        <tem:credentials>
                            <vec:IdKey>{_idKey}</vec:IdKey>
                            <vec:KundId>{_kundId}</vec:KundId>
                            <vec:Lk>{_lk}</vec:Lk>
                            <vec:Password>{hashedPassword}</vec:Password>
                            <vec:ProduktId>{_produktId}</vec:ProduktId>
                        </tem:credentials>
                        <tem:regnr>{licensePlate}</tem:regnr>
                    </tem:GetRegnr>
                </soapenv:Body>
            </soapenv:Envelope>";

            Console.WriteLine($"🔹 SOAP Request Sent:\n{soapRequest}");

            var requestContent = new StringContent(soapRequest, Encoding.UTF8, "text/xml");
            requestContent.Headers.Add("SOAPAction", "http://tempuri.org/IVeCloudService/GetRegnr"); // ✅ REQUIRED

            var response = await _httpClient.PostAsync(_baseUrl, requestContent);

            if (!response.IsSuccessStatusCode)
            {
                string errorResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"❌ API Request Failed - Status {response.StatusCode}: {errorResponse}");
                throw new Exception($"API Request failed with status {response.StatusCode}");
            }

            var responseXml = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"🔹 RAW API Response: {responseXml}");

            Console.WriteLine($"🔹 FULL API RESPONSE:\n{responseXml}");


            return responseXml; // ✅ Return raw XML response to be parsed
        }

        private string HashPassword(string regnr, string password)
        {
            return MD5Hash(regnr + MD5Hash(password));
        }

        private static string MD5Hash(string input)
        {
            using var md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToUpper();
        }
    }
}
