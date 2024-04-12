using Microsoft.AspNetCore.Mvc;
using AgileWorks_Carmen_Maar.Model;
namespace AgileWorks_Carmen_Maar.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InquiryController : ControllerBase
{
    private readonly DataContext _context;

    public InquiryController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetInquiries()
    {
        var inquiries = _context.InquiryList.ToList();
        
        if (!inquiries.Any())
        {
            var seededInquiries = new List<Inquiry>
            {
                new Inquiry
                {
                    Id = 1,
                    Description = "Veebilehe arendus",
                    InsertionTime = new DateTime(2024, 3, 2, 13, 43, 55),
                    Deadline = new DateTime(2024, 4, 13, 19, 0, 0)
                },
                new Inquiry
                {
                    Id = 2,
                    Description = "Powerpoint esitlus",
                    InsertionTime = new DateTime(2024, 3, 21, 21, 0, 0),
                    Deadline = new DateTime(2024, 3, 26, 10, 30, 0)
                }
            };
        
            _context.InquiryList.AddRange(seededInquiries);
            _context.SaveChanges();
        
            inquiries.AddRange(seededInquiries);
        }

        return Ok(inquiries);
    }

    [HttpPost]
    public IActionResult AddInquiry([FromBody] InquiryDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!DateTime.TryParse(model.Deadline, out var deadlineDate))
        {
            return BadRequest("Invalid deadline format. Please provide a valid date format.");
        }

        var newInquiry = new Inquiry
        {
            Description = model.Description,
            InsertionTime = DateTime.Now,
            Deadline = deadlineDate
        };

        try
        {
            _context.InquiryList.Add(newInquiry);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }

        return Ok(newInquiry);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteInquiry(int id)
    {
        var inquiry = _context.InquiryList.FirstOrDefault(i => i.Id == id);
        if (inquiry == null)
        {
            return NotFound();
        }

        try
        {
            _context.InquiryList.Remove(inquiry);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }

        return NoContent();
    }
}

public class InquiryDto
{
    public string? Description { get; set; }
    public string? Deadline { get; set; }
}