using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Python.Runtime;
using Microsoft.AspNetCore.Mvc;
using Mysitemvc.Models;

public class MyController : Controller
{
    private readonly HttpClient _httpClient;

    public MyController()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5000/"); // Update with your Flask API base URL    
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync("items");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<List<Item>>(content);
                return View(items);
            }
            else
            {
                // Handle error
                return View("Error");
            }
        }
        catch (Exception ex)
        {
            // Log the exception or handle it accordingly
            return View("Error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> UpdateItem(int id, string username, string password)
    {
        try
        {
            // Prepare the data to be sent as JSON
            var requestData = new
            {
                username,
                password
            };

            // Serialize the data to JSON
            var jsonContent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

            // Make the PUT request to the Flask API
            HttpResponseMessage response = await _httpClient.PutAsync($"api/items/{Id}", jsonContent);

            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Read and parse the response content
                var responseData = await response.Content.ReadAsStringAsync();
                var updatedItem = JsonConvert.DeserializeObject<Item>(responseData);

                // Optionally, you can return a view or perform other actions
                return View("UpdateSuccess", updatedItem);
            }
            else
            {
                // Handle error
                return View("UpdateError");
            }
        }
        catch (Exception ex)
        {
            // Log the exception or handle it accordingly
            return View("Error");
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"items/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<Item>(content);
                return View(item);
            }
            else
            {
                // Handle error
                return View("Error");
            }
        }
        catch (Exception ex)
        {
            // Log the exception or handle it accordingly
            return View("Error");
        }
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"items/{id}");

            if (response.IsSuccessStatusCode)
            {
                // Handle success, e.g., redirect to Index
                return RedirectToAction("Index");
            }
            else
            {
                // Handle error
                return View("Error");
            }
        }
        catch (Exception ex)
        {
            // Log the exception or handle it accordingly
            return View("Error");
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"items/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<Item>(content);
                return View(item);
            }
            else
            {
                // Handle error
                return View("Error");
            }
        }
        catch (Exception ex)
        {
            // Log the exception or handle it accordingly
            return View("Error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Item updatedItem)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"items/{id}", updatedItem);

            if (response.IsSuccessStatusCode)
            {
                // Handle success, e.g., redirect to Index
                return RedirectToAction("Index");
            }
            else
            {
                // Handle error
                return View("Error");
            }
        }
        catch (Exception ex)
        {
            // Log the exception or handle it accordingly
            return View("Error");
        }
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Item newItem)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("items", newItem);

            if (response.IsSuccessStatusCode)
            {
                // Handle success, e.g., redirect to Index
                return RedirectToAction("Index");
            }
            else
            {
                // Handle error
                return View("Error");
            }
        }
        catch (Exception ex)
        {
            // Log the exception or handle it accordingly
            return View("Error");
        }
    }
}
