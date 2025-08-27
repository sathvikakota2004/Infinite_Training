using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ContactManager.Models;
using ContactManager.Repositories;

namespace ContactManager.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepository _repository;

        public ContactController()
        {
            
            _repository = new ContactRepository(new ContactContext());
        }

        
        public async Task<ActionResult> Index()
        {
            var contacts = await _repository.GetAllAsync();
            return View(contacts);
        }

       
        public ActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                await _repository.CreateAsync(contact);
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        
        public async Task<ActionResult> Delete(long id)
        {
            var contacts = await _repository.GetAllAsync();
            var contact = contacts.FirstOrDefault(c => c.Id == id);

            if (contact == null)
                return HttpNotFound();

            return View(contact);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
