namespace GroceryStoreApi.Model
{
    /// <summary>
    /// Customer
    /// </summary>
    public class Customer:ICustomer
    {
        /// <summary>
        /// Customer Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// First Name of the customer
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last Name of the customer
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Gender of the customer
        /// </summary>
        public string Gender { get; set; }

        public bool IsValid()
        {
            //TO DO: implement validations for Customer entity
            //For now only adding First name, last name and Gender required
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(Gender))
                return false;
            return true;
        }

        public void Update(ICustomer updated)
        {
            this.FirstName = updated.FirstName;
            this.LastName = updated.LastName;
            this.Gender = updated.Gender;
        }
    }
}
