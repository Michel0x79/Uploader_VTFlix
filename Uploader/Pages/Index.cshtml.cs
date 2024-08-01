using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VideoUploadApp.Pages
{
    public class UploadModel : PageModel
    {
        [BindProperty]
        public IFormFile File { get; set; }
        public string Message { get; private set; }

        public string testador { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Message = "mensagem inicial";
            
            if (File == null || File.Length == 0)
            {
                Message = "Nenhum arquivo selecionado.";
                return Page();
            }
            var extension = Path.GetExtension(File.FileName).ToLower();
            if (!extension.Equals(".zip"))
            {
                Message = "O arquivo deve ser compactado em .zip";
                return Page();
            }
                
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedVideos", File.FileName);

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await File.CopyToAsync(stream);
                }

                Message = "Upload realizado com sucesso!";
            }
            catch (Exception e)
            {

                Message = "Ococorreu um erro \n " + e.Message;
            }

            return Page();

        }
    }
}
