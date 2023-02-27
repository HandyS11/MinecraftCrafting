// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InventoryController.cs" company="UCA Clermont-Ferrand">
//     Copyright (c) UCA Clermont-Ferrand All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Minecraft.Crafting.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Minecraft.Crafting.Api.Models;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Xml.Linq;

    /// <summary>
    /// The inventory controller.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private static Mutex mtx = new Mutex(false);
        /// <summary>
        /// The json serializer options.
        /// </summary>
        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
        };

        private List<InventoryModel> retryIOGet(int tries)
        {
            for (int i = 0; i < tries; i++)
            {
                try
                {
                    return JsonSerializer.Deserialize<List<InventoryModel>>(System.IO.File.ReadAllText("Data/inventory.json"), _jsonSerializerOptions);
                }
                catch (IOException ex)
                {
                    Thread.Sleep(100);  //retry
                }
            }
            return null;
        }

        private void retryIOSet(List<InventoryModel> data, int tries)
        {
            for (int i = 0; i < tries; i++)
            {
                try
                {
                    System.IO.File.WriteAllText("Data/inventory.json", JsonSerializer.Serialize(data, _jsonSerializerOptions));
                }
                catch (IOException ex)
                {
                    Thread.Sleep(100);  //retry
                }
            }
        }

        /// <summary>
        /// Adds to inventory.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The async task.</returns>
        [HttpPost]
        [Route("")]
        public Task AddToInventory(InventoryModel item)
        {
            mtx.WaitOne();
            if (!System.IO.File.Exists(@"Data/inventory.json"))
            {
                System.IO.File.Create(@"Data/inventory.json").Close();
                var ddata = new List<InventoryModel>();
                retryIOSet(ddata, 10);
            }
            List<InventoryModel> data = retryIOGet(10);

            if (data == null)
            {
                throw new Exception("Unable to get the inventory.");
            }
            data.Add(item);

            retryIOSet(data, 10);
            mtx.ReleaseMutex();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Deletes from inventory.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The async task.</returns>
        [HttpDelete]
        [Route("")]
        public Task DeleteFromInventory(InventoryModel item)
        {
            mtx.WaitOne();
            if (!System.IO.File.Exists("Data/inventory.json"))
            {
                throw new Exception($"Unable to found the item with name: {item.ItemName}");
            }
            List<InventoryModel> data = retryIOGet(10);


            if (data == null)
            {
                throw new Exception("Unable to get the inventory.");
            }

            var inventoryItem = data.FirstOrDefault(w => w.ItemName == item.ItemName && w.Position == item.Position);

            if (inventoryItem == null)
            {
                throw new Exception($"Unable to found the item with name: {item.ItemName} at position: {item.Position}");
            }

            data.Remove(inventoryItem);

            retryIOSet(data, 10);
            mtx.ReleaseMutex();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Gets the inventory.
        /// </summary>
        /// <returns>The inventory.</returns>
        [HttpGet]
        [Route("")]
        public Task<List<InventoryModel>> GetInventory()
        {
            mtx.WaitOne();
            if (!System.IO.File.Exists("Data/inventory.json"))
            {
                return Task.FromResult(new List<InventoryModel>());
            }

            List<InventoryModel> data = retryIOGet(10);

            if (data == null)
            {
                throw new Exception("Unable to get the inventory.");
            }
            mtx.ReleaseMutex();
            return Task.FromResult(data);
        }

        /// <summary>
        /// Updates the inventory.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The async task.</returns>
        [HttpPut]
        [Route("")]
        public Task UpdateInventory(InventoryModel item)
        {
            mtx.WaitOne();
            var data = retryIOGet(10);

            if (data == null)
            {
                throw new Exception("Unable to get the inventory.");
            }

            var inventoryItem = data.FirstOrDefault(w => w.ItemName == item.ItemName && w.Position == item.Position);

            if (inventoryItem == null)
            {
                throw new Exception($"Unable to found the item with name: {item.ItemName} at position: {item.Position}");
            }

            inventoryItem.ItemName = item.ItemName; //inutiles
            inventoryItem.Position = item.Position; //hein???
            inventoryItem.NumberItem = item.NumberItem;  //rajouté

            retryIOSet(data, 10);
            mtx.ReleaseMutex();
            return Task.CompletedTask;
        }
    }
}