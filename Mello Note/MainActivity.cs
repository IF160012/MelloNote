using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Mello_Note
{
    [Activity(Label = "Mello_Note"/*, Icon = "@drawable/icon"*/, MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }
        public void btnOneClick()
        {
            throw new Exception();
        }

        public class Note
        {
            public string Id { get; set; }
            public string Text { get; set; }
            public string Level { get; set; }
        }

        static Mello_Note = new MelloNotes();

        static void ShowNote(Note Notes)
        {
            Console.WriteLine($"Id: {Notes.Id}\tText: " +
                $"{Notes.Text}\tCategory: {Notes.Level}");
        }

        static async Task<Uri> CreateProductAsync(Note Notes)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/notes", Notes);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        static async Task<Note> GetProductAsync(string path)
        {
            Note Notes = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                Notes = await response.Content.ReadAsAsync<Note>();
            }
            return Notes;
        }

        static async Task<Note> UpdateProductAsync(Note Notes)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/notes/{Notes.Id}", Notes);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated Notes from the response body.
            Notes = await response.Content.ReadAsAsync<Note>();
            return Notes;
        }

        static async Task<HttpStatusCode> DeleteProductAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/notes/{id}");
            return response.StatusCode;
        }

        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:64195/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Create a new note
                Note Notes = new Note
                {
                    Id = "1",
                    Text = "Gizmo is cute",
                    Level = "2"
                };

                var url = await CreateProductAsync(Notes);
                Console.WriteLine($"Created at {url}");

                // Get the note
                Notes = await GetProductAsync(url.PathAndQuery);
                ShowNote(Notes);

                // Update the note
                Console.WriteLine("Updating note level...");
                Notes.Level = "3";
                await UpdateProductAsync(Notes);

                // Get the updated note
                Notes = await GetProductAsync(url.PathAndQuery);
                ShowNote(Notes);

                // Delete the note
                var statusCode = await DeleteProductAsync(Notes.Id);
                Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

    }



}

