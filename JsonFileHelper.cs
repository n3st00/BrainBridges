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
                List<object> existingList = JsonConvert.DeserializeObject<List<object>>(existingData) ?? new List<object>(); // create an empty
                                                                                                                             //collection if it's necessary
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
                StorageFile file = await localfolder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
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
                StorageFile file = await localfolder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
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
            // specify json beginning structure
            object startJSON = new { ProjectName = filename, ProjectDescription = "" }; //description gets cleared
                                                                                        // fix?
            try
            {
                StorageFolder localfolder = ApplicationData.Current.LocalFolder;
                StorageFile file = await localfolder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
                List<object> existingList = new List<object>();
                existingList.Add(startJSON);
                string updatedJSON = JsonConvert.SerializeObject(existingList);
                await FileIO.WriteTextAsync(file, updatedJSON);
                System.Diagnostics.Debug.WriteLine("Succesfully cleared the JSON file.");
            } catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error clearing JSON file: {ex.Message}");
            }
           
        }

        public static async Task CreateBlankJsonFile(string filename)
        {
            try
            {
                StorageFolder localfolder = ApplicationData.Current.LocalFolder;
                StorageFile file = await localfolder.TryGetItemAsync(filename) as StorageFile;
                if (file == null)
                {
                    System.Diagnostics.Debug.WriteLine($"There is no such file as '{filename}'");
                } else
                {
                    await FileIO.WriteTextAsync(file, "");
                    System.Diagnostics.Debug.WriteLine("Succesfully created a file.");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating a JSON file: {ex.Message}");
            }
        }

        public static async Task TestCreateNewBlank(string filename)
        {
            try
            {
                StorageFolder localfolder = ApplicationData.Current.LocalFolder;
                StorageFile file = await localfolder.TryGetItemAsync(filename) as StorageFile;
                if (file == null)
                {
                    System.Diagnostics.Debug.WriteLine($"There is no such file as '{filename}'");
                }
                else
                {
                    await FileIO.WriteTextAsync(file, "");
                    System.Diagnostics.Debug.WriteLine("Succesfully created a file.");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating a JSON file: {ex.Message}");
            }
        }

        public static async Task TestReadFromJson(string filename)
        {
            try
            {
                StorageFolder localfolder = ApplicationData.Current.LocalFolder;
                StorageFile file = await localfolder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
                string jsonData = await FileIO.ReadTextAsync(file);
                System.Diagnostics.Debug.WriteLine("JSON data read from file:");
                System.Diagnostics.Debug.WriteLine(jsonData);
                await file.DeleteAsync();
                return;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reading file: {ex.Message}");
                return;
            }
        }
    }
}
