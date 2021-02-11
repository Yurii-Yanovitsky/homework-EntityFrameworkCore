using System.Collections.Generic;

namespace Task2
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Phone> Phones { get; set; }
        public Company()
        {
            Phones = new List<Phone>();
        }
    }

}









//1)
//CREATE PROCEDURE[dbo].[GetCarsByID_1]
//@paramId int,
// @Id int out,
// @Name varchar(25) out,
// @Price int out
// AS
// SELECT @ID = c.Id, @Name = c.[Name], @Price = c.Price FROM Cars as c
// WHERE c.Id = @paramId
// GO


//2)
//Create PROCEDURE[dbo].[GetCarsByID_2]
//@paramId int
//AS
// SELECT* FROM Cars as c
// WHERE c.Id = @paramId
// GO

// exec GetCarsByID_2 10
// GO
