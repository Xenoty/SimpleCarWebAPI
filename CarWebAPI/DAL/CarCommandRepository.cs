using CarWebAPI.Helpers;
using CarWebAPI.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace CarWebAPI.Services
{
    public interface ICarCommandRepository
    {
        void CreateMake(string type);
        void CreateModelForSpecificMake(string model, int makeId);
        void UpdateCarModel(CarModel carModel);
        void UpdateCarMake(CarMake carMake);
        void RemoveMake(int makeId);
        void RemoveModel(int modelId);
    }

    public class CarCommandRepository : ICarCommandRepository
    {
        private string _carMakesFilePath;
        private string _carModelsFilePath;

        public CarCommandRepository(IConfiguration configuration)
        {
            _carMakesFilePath = configuration.GetValue<string>("DataSettings:CarMakes");
            _carModelsFilePath = configuration.GetValue<string>("DataSettings:CarModels");
        }

        public void CreateMake(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new ApiException("Field 'Type' cannot be null or empty. Please enter a value.");
            }

            string lastLine = File.ReadLines(_carMakesFilePath).Last();
            string[] currentRowAttributes = lastLine.Split(",").ToArray();
            int newCareMakeId = int.Parse(currentRowAttributes[0]) + 1;

            string newCareMakeEntry = newCareMakeId +  "," + type;

            File.AppendAllText(_carMakesFilePath, Environment.NewLine + newCareMakeEntry);
        }

        public void CreateModelForSpecificMake(string model, int makeId)
        {
            if (string.IsNullOrEmpty(model))
            {
                throw new ApiException("Field 'model' cannot be null or empty. Please enter a value.");
            }

            if (makeId <= 0)
            {
                throw new ApiException("Field 'makeId' cannot be 0 or less than 0. Please enter a value.");
            }

            // Validate if makeID exists
            bool makeIdExists = false;
            using(StreamReader streamReader = new StreamReader(_carMakesFilePath))
            {
                string row = null;
                while ((row = streamReader.ReadLine()) != null)
                {
                    string[] currentRow = row.Split(",");
                    if (int.Parse(currentRow[0]) == makeId)
                    {
                        makeIdExists = true;
                        break;
                    }
                }
                streamReader.Close();
            }

            if (!makeIdExists)
            {
                throw new ApiException("Could not find Car Make with MakeId '" + makeId + "'.");
            }

            string lastLine = File.ReadLines(_carModelsFilePath).Last();
            string[] currentRowAttributes = lastLine.Split(",").ToArray();
            int newCareMakeId = int.Parse(currentRowAttributes[0]) + 1;

            string newCareModelEntry = newCareMakeId + "," + model + "," + makeId;

            File.AppendAllText(_carModelsFilePath, Environment.NewLine + newCareModelEntry);
        }

        public void RemoveMake(int makeId)
        {
            if (makeId <= 0)
            {
                throw new ApiException("Field 'makeId' cannot be 0 or less than 0. Please enter a value.");
            }

            bool isCarMakeIDFound = false;

            using (StreamReader streamReader = new StreamReader(_carMakesFilePath))
            {
                string row = null;
                string lines = "";
                int counter = 1;

                while ((row = streamReader.ReadLine()) != null)
                {
                    string[] currentRowAttributes = row.Split(",");
                    if (int.Parse(currentRowAttributes[0]) != makeId)
                    {
                        if (counter == 1)
                        {
                            lines += currentRowAttributes[0] + "," + currentRowAttributes[1];
                        }
                        else
                        {
                            lines += Environment.NewLine + currentRowAttributes[0] + "," + currentRowAttributes[1];
                        }
                    }
                    else
                    {
                        isCarMakeIDFound = true;
                    }

                    counter++;
                }
                streamReader.Close();

                if (!isCarMakeIDFound)
                {
                    throw new ApiException("Car Make not found with MakeID '" + makeId + "'.");
                }

                File.WriteAllText(_carMakesFilePath, lines);
            }
        }

        public void RemoveModel(int modelId)
        {
            if (modelId <= 0)
            {
                throw new ApiException("Field 'modelId' cannot be 0 or less than 0. Please enter a value.");
            }

            bool isCarModelIDFound = false;

            using (StreamReader streamReader = new StreamReader(_carModelsFilePath))
            {
                string row = null;
                string lines = "";
                int counter = 1;

                while ((row = streamReader.ReadLine()) != null)
                {
                    string[] currentRowAttributes = row.Split(",");

                    if (int.Parse(currentRowAttributes[0]) != modelId)
                    {
                        if (counter == 1)
                        {
                            lines += currentRowAttributes[0] + "," + currentRowAttributes[1] + "," + currentRowAttributes[2];
                        }
                        else
                        {
                            lines += Environment.NewLine + currentRowAttributes[0] + "," + currentRowAttributes[1] + "," + currentRowAttributes[2];
                        }
                    }
                    else
                    {
                        isCarModelIDFound = true;
                    }

                    counter++;
                }
                streamReader.Close();

                if (!isCarModelIDFound)
                {
                    throw new ApiException("Car Model not found with ID '" + modelId + "'.");
                }

                File.WriteAllText(_carModelsFilePath, lines);
            }
        }

        public void UpdateCarMake(CarMake carMake)
        {
            if (carMake.MakeID <= 0)
            {
                throw new ApiException("Field 'MakeID' cannot 0 or less than 0. Please enter a value");
            }

            if (string.IsNullOrEmpty(carMake.Type))
            {
                throw new ApiException("Field 'Type' cannot be null or empty. Please enter a value");
            }

            bool isCarMakeIDFound = false;

            string lines = "";

            using (StreamReader streamReader = new StreamReader(_carMakesFilePath))
            {
                string row = null;
                int lastLine = File.ReadLines(_carMakesFilePath).Count();
                int counter = 1;

                while ((row = streamReader.ReadLine()) != null)
                {
                    string[] currentRowAttributes = row.Split(",");
                    if (int.Parse(currentRowAttributes[0]) == carMake.MakeID)
                    {
                        lines += currentRowAttributes[0] + "," + carMake.Type;
                        isCarMakeIDFound = true;
                    }
                    else
                    {
                        lines += row;
                    }

                    if (counter != lastLine)
                    {
                        lines += Environment.NewLine;
                    }
                    counter++;
                }
                streamReader.Close();
            }

            if (!isCarMakeIDFound)
            {
                throw new ApiException("Car Make not found with MakeID '" + carMake.MakeID + "'.");
            }

            File.WriteAllText(_carMakesFilePath, lines);
        }

        public void UpdateCarModel(CarModel carModel)
        {
            if (carModel.ID <= 0)
            {
                throw new ApiException("Field 'ID' cannot 0 or less than 0. Please enter a value");
            }

            if (string.IsNullOrEmpty(carModel.Model))
            {
                throw new ApiException("Field 'Model' cannot be null or empty. Please enter a value");
            }

            if (carModel.MakeID <= 0)
            {
                throw new ApiException("Field 'MakeID' cannot 0 or less than 0. Please enter a value");
            }

            bool isCarModelIDFound = false;
            string lines = "";

            using (StreamReader streamReader = new StreamReader(_carModelsFilePath))
            {
                string row = null;
                int lastLine = File.ReadLines(_carModelsFilePath).Count();
                int counter = 1;

                while ((row = streamReader.ReadLine()) != null)
                {
                    string[] currentRowAttributes = row.Split(",");
                    if (int.Parse(currentRowAttributes[0]) == carModel.ID)
                    {
                        lines += currentRowAttributes[0] + "," + carModel.Model + "," + carModel.MakeID;
                        isCarModelIDFound = true;
                    }
                    else
                    {
                        lines += row;
                    }

                    if (counter != lastLine)
                    {
                        lines += Environment.NewLine;
                    }
                    counter++;
                }
                streamReader.Close();
            }

            if (!isCarModelIDFound)
            {
                throw new ApiException("Car Model not found with ID '" + carModel.ID + "'.");
            }

            File.WriteAllText(_carModelsFilePath, lines);
        }
    }
}
