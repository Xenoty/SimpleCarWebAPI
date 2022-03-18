using CarWebAPI.Helpers;
using CarWebAPI.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace CarWebAPI.Services
{
    public interface ICarQueryService
    {
        List<CarMake> GetAllMakes();
        List<CarModel> GetAllModels();
        CarModel GetModelById(int modelId);
        CarMake GetMakeById(int makeId);
    }

    public class CarQueryService : ICarQueryService
    {
        private string _carMakesFilePath;
        private string _carModelsFilePath;

        public CarQueryService(IConfiguration configuration)
        {
            _carMakesFilePath = configuration.GetValue<string>("DataSettings:CarMakes");
            _carModelsFilePath = configuration.GetValue<string>("DataSettings:CarModels");
        }

        public List<CarMake> GetAllMakes()
        {
            List<CarMake> allMakesList = new List<CarMake>();

            using (StreamReader file = new StreamReader(_carMakesFilePath))
            {
                string row = null;

                while ((row = file.ReadLine()) != null)
                {
                    string[] currentRowAttributes = row.Split(",");
                    CarMake makeModel = new CarMake()
                    {
                        MakeID = int.Parse(currentRowAttributes[0]),
                        Type = currentRowAttributes[1],
                    };
                    allMakesList.Add(makeModel);
                }

                file.Close();
            }

            return allMakesList;
        }

        public List<CarModel> GetAllModels()
        {
            List<CarModel> allCarModels = new List<CarModel>();

            using (StreamReader file = new StreamReader(_carModelsFilePath))
            {
                string row = null;
                while ((row = file.ReadLine()) != null)
                {
                    string[] currentRowAttributes = row.Split(",");
                    CarModel carModel = new CarModel()
                    {
                        ID = int.Parse(currentRowAttributes[0]),
                        Model = currentRowAttributes[1],
                        MakeID = int.Parse(currentRowAttributes[2])
                    };
                    allCarModels.Add(carModel);
                }
                file.Close();
            }

            return allCarModels;
        }

        public CarMake GetMakeById(int makeId)
        {
            using (StreamReader file = new StreamReader(_carMakesFilePath))
            {
                string row = null;
                while ((row = file.ReadLine()) != null)
                {
                    string[] currentRowAttributes = row.Split(",");
                    if (int.Parse(currentRowAttributes[0]) == makeId)
                    {
                        return new CarMake()
                        {
                            MakeID = int.Parse(currentRowAttributes[0]),
                            Type = currentRowAttributes[1]
                        };
                    }
                }
                file.Close();
            }

            throw new ApiException("Car Make not found with MakeID '" + makeId + "'.");
        }

        public CarModel GetModelById(int modelId)
        {
            using (StreamReader file = new StreamReader(_carModelsFilePath))
            {
                string row = null;
                while ((row = file.ReadLine()) != null)
                {
                    string[] currentRowAttributes = row.Split(",");
                    if (int.Parse(currentRowAttributes[0]) == modelId)
                    {
                        return new CarModel()
                        {
                            ID = int.Parse(currentRowAttributes[0]),
                            Model = currentRowAttributes[1],
                            MakeID = int.Parse(currentRowAttributes[2])
                        };
                    }
                }
                file.Close();
            }

            throw new ApiException("Car Model not found with ID '" + modelId + "'.");
        }
    }
}
