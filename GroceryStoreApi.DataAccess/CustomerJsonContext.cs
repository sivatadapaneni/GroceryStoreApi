using System;
using GroceryStoreApi.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreApi.DataAccess
{
    /// <summary>
    /// Customer JSON Context
    /// Customer context uses JSON as data store
    /// </summary>
    public class CustomerJsonContext: ICustomerContext
    {
        //TO DO: Configure to read file path from appSettings 
        readonly string _fileName = "./database.json";
        static object _lockObj = new object();


        /// <summary>
        /// CustomerJsonContext 
        /// </summary>
        public CustomerJsonContext() 
        {
        }

        /// <summary>
        /// Reads data from json file and deserialize the data to customers 
        /// </summary>
        /// <returns>returns all customers</returns>
        public async Task<List<Customer>> ReadFileAsync()
        {

            //Read JSON file content and deserialize the content to List of Cusotmer objects
            using (StreamReader r = new StreamReader(_fileName))
            {
                var json = await r.ReadToEndAsync();
                var serializerSettings = new JsonSerializerSettings();
                serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                return JsonConvert.DeserializeObject<List<Customer>>(json);
            }
        }

        /// <summary>
        /// Writes customers data back to file
        /// </summary>
        /// <param name="Customer">Customers to Write</param>
        /// <returns>returns status</returns>
        public async void WriteFileAsync(List<Customer> customers)
        {
            //TO DO: instead of rewriting the entire file each time append only changes

            lock (_lockObj)
            {
                string jsonText = JsonConvert.SerializeObject(customers);
                File.WriteAllText(_fileName, jsonText);
            }
        }
    }
}
