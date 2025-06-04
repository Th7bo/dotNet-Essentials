namespace Reservation
{
    public static class Validation
    {
        // ToDo: validate the national insurance number. Throw a validationException if the
        //       national insurance number is not correct.
        public static string Validate(string nationalInsuranceNumber)
        {
            if (string.IsNullOrWhiteSpace(nationalInsuranceNumber) || nationalInsuranceNumber.Length != 11 || !long.TryParse(nationalInsuranceNumber, out long number))
            {
                throw new ValidationException("Ongeldig rijksregisternummer (11 cijfers vereist).");
            }

            long first9 = long.Parse(nationalInsuranceNumber.Substring(0, 9));
            int last2 = int.Parse(nationalInsuranceNumber.Substring(9, 2));

            // First try pre-2000 rule
            int mod = (int)(first9 % 97);
            int check = 97 - mod;

            if (check == last2)
            {
                return nationalInsuranceNumber; // OK
            }

            // Then try post-2000 rule
            mod = (int)((2000000000L + first9) % 97);
            check = 97 - mod;

            if (check == last2)
            {
                return nationalInsuranceNumber; // OK
            }

            throw new ValidationException("Ongeldig rijksregisternummer (mod97 check mislukt).");
        }

    }
}
