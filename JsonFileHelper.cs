using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;


namespace BrainBridges
{
    internal class JsonFileHelper
    {
        public static async Task WriteToJsonFile(string filename, object newData)
        {
            try
            {
                // get the local folder
                StorageFolder localfolder = ApplicationData.Current.LocalFolder;
                // Get the file
                StorageFile file = await localfolder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
                // Read the existing json data
                string existingData = await FileIO.ReadTextAsync(file);

                // Deserialize the existing json data into a collection
                List<object> existingList = JsonConvert.DeserializeObject<List<object>>(existingData) ?? new List<object>();
                // add new data to the list
                existingList.Add(newData);
                string updatedData = JsonConvert.SerializeObject(existingList);
                await FileIO.WriteTextAsync(file, updatedData);
                System.Diagnostics.Debug.WriteLine("Succesfully wrote to the json file.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Couldn't write to json file: {ex.Message}");
            }
        }

        public static async Task ReadFromJsonFile(string filename)
        {
            try
            {
                StorageFolder localfolder = ApplicationData.Current.LocalFolder;
                StorageFile file = await localfolder.GetFileAsync(filename);
                string jsonData = await FileIO.ReadTextAsync(file);
                System.Diagnostics.Debug.WriteLine("JSON data read from file:");
                System.Diagnostics.Debug.WriteLine(jsonData);

                return;
            } catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reading file: {ex.Message}");
                return;
            }
        }

        public static async Task WriteObjectToJson(string filename, PointStr point)
        {
            try
            {
                StorageFolder localfolder = ApplicationData.Current.LocalFolder;
                StorageFile file = await localfolder.GetFileAsync(filename);
                string existingData = await FileIO.ReadTextAsync(file);
                List<object> existingList = JsonConvert.DeserializeObject<List<object>>(existingData) ?? new List<object>();
                existingList.Add(point);
                string updatedData = JsonConvert.SerializeObject(existingList);
                await FileIO.WriteTextAsync(file, updatedData);
                System.Diagnostics.Debug.WriteLine("Succesfulle written the object to JSON file.");
            } catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{ex.Message}");
            }
            

        }

        public static async Task ClearJsonFile(string filename)
        {
            try
            {
                StorageFolder localfolder = ApplicationData.Current.LocalFolder;
                StorageFile file = await localfolder.GetFileAsync(filename);
                await FileIO.WriteTextAsync(file, "");
                System.Diagnostics.Debug.WriteLine("Succesfully cleared the JSON file.");
            } catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error clering JSON file: {ex.Message}");
            }
           
        }
    }
}
