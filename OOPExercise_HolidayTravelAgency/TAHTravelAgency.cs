using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPExercise_HolidayTravelAgency
{
    // To write an internal user application for a Holiday Travel Agency
    // Can play/ Explore further with the objects created

        // My Attempt: 

    class TAHTourAgency
    {
        static void Main (string [] args)
        { 
        TravelAgency t = new TravelAgency("Tan Ah Huat Travel Far");
        t.Add(new Customer("Tan Lian Hwee", "Clementi Road", "C10010"));
            t.Add(new Customer("Lim Teck Gee", "Kent Ridge Road", "C10020"));
            t.Add(new Customer("Koh Ghim Moh", "Dover Road", "C10030"));
            t.Add(new Customer("Liat Kim Ho", "West Coast Road", "C10040"));
            t.Add(new Tour("Paris", 3400, 3));
            t.Add(new Tour("London", 3200, 3));
            t.Add(new Tour("Munich", 3100, 2));
            t.Add(new Tour("Milan", 3500, 3));

            Console.WriteLine($"Welcome to {t.name}\n");
            t.PrintCustomers();


            TourPackage p = new TourPackage("Europe");
        p.ConsistOf(t.FindTour("London"));
            p.ConsistOf(t.FindTour("Paris"));

            t.Add(p);
            t.Add(new Trip(t.FindTour("Paris"), new DateTime(2015, 4, 2), 20));
            t.Add(new Trip(t.FindTour("Munich"), new DateTime(2015, 4, 8), 15));
            t.Add(new Trip(t.FindTour("Europe"), new DateTime(2015, 4, 12), 17));

            
            t.MakeBooking(new Booking(t.FindCustomer("Lim Teck Gee"), t.FindTrip("Paris"), 7));
            t.MakeBooking(new Booking(t.FindCustomer("Liat Kim Ho"), t.FindTrip("Europe"), 2));
            t.MakeBooking(new Booking(t.FindCustomer("Koh Ghim Moh"), t.FindTrip("Munich"), 1));
            t.MakeBooking(new Booking(t.FindCustomer("Tan Lian Hwee"), t.FindTrip("Europe"), 3));
            

            t.ListTours();
            t.ListTrips();

         }
    }


    public class Customer
    {
        public string customerName;
        public string customerAddress;
        private string Id;

        public Customer()
        {

        }

        public Customer(string c, string a, string i)
        {
            this.customerName = c;
            this.customerAddress = a;
            this.Id = i;
        }

        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }

        public string CustomerAddress
        {
            get { return customerAddress; }
            set { customerAddress = value; }
        }

        public string ID
        {
            get { return Id; }
        }
    }

    public class TourGuide
    {
        public string tourGuideName;
        public string tourGuideAddress;
        private int salary;

        public TourGuide()
        {

        }

        public TourGuide(string t, string k, int q)
        {
            this.tourGuideName = t;
            this.tourGuideAddress = k;
            this.salary = q;
        }

        public string TourGuideName
        {
            get { return tourGuideName; }
            set { tourGuideName = value; }
        }

        public string TourGuideAddress
        {
            get { return tourGuideAddress; }
            set { tourGuideAddress = value; }
        }

        public int Salary
        {
            get { return salary; }
            set { salary = value; }
        }
    }

    public class Tour
    {
        public string tourName;
        public int tourCost;
        public int tourDuration;
        public List<string> placesOfInterest = new List<string>();

        public Tour()
        {

        }


        public Tour(string tN, int tC, int tD)
        {
            this.tourName = tN;
            this.tourCost = tC;
            this.tourDuration = tD;
        }

        public string TourName
        {
            get { return tourName; }
        }

        public virtual int TourCost
        {
            get { return tourCost; }

            // can't be updated
            set { }

        }

        public virtual int TourDuration
        {
            get { return tourDuration; }

            // can't be updated
            set { }
        }

        public override string ToString()
        {
            return String.Format("Tour Name= {0}, Tour Cost= {1}, Tour Duration= {2}]", this.tourName, this.tourCost, this.tourDuration);
        }
    }

    public class TourPackage : Tour
    {
        public List<Tour> listOfTours = new List<Tour>();
        public string country;

        public TourPackage() : base()
        {

        }

        public TourPackage(string c)
        {
            this.country = c;
        }

        public void ConsistOf(Tour tour)
        {
            listOfTours.Add(tour);
        }

        public override int TourCost
        {
            get { return tourCost; }
            set
            {
                int tourPackagePrice = 0;

                for (int g = 0; g < listOfTours.Count; g++)
                {
                    Tour tour02 = listOfTours[g];
                    tourPackagePrice += tour02.tourCost;
                }
                tourCost = Convert.ToInt32(tourPackagePrice * 0.9);
            }
        }

        public override int TourDuration
        {
            get { return tourDuration; }

            set
            {
                int tourPackageDuration = 0;
                for (int j = 0; j < listOfTours.Count; j++)
                {
                    Tour tour01 = listOfTours[j];
                    tourPackageDuration += tour01.tourDuration;
                }
                tourDuration = tourPackageDuration;
            }
        }
    }

    public class Trip
    {
        public int maximumSize;

        public DateTime when = new DateTime();

        public Tour tourTrip = new Tour();

        public List<Booking> Bookings = new List<Booking>();


        public Trip()
        {

        }

        public Trip(Tour t, DateTime w1, int m)
        {
            this.tourTrip = t;
            this.when = w1;
            this.maximumSize = m;
        }

        // to amend
        public void Book(Customer customer, int N)
        {
            for (int j = 0; j < Bookings.Count; j++)
            {
                Booking n0 = new Booking();
                n0.bookingCustomer = customer;


                if (N > maximumSize)
                {
                    try
                    {
                        throw new ApplicationException
                    ("Number of seats requested for booking exceeds the maximum number of placings available for this trip!");


                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception");
                        Console.WriteLine(e.Message);

                        return;
                    }
                }
                else
                {
                    n0.numberOfTravellers = N;
                }

            }

        }

        public double GetRevenue()
        {
            double revenue = 0;

            for (int p = 0; p < Bookings.Count; p++)
            {
                Booking n = new Booking();
                revenue += n.cost;
            }

            return revenue;
        }

    }

    public class Booking
    {
        public Customer bookingCustomer = new Customer();
        public Trip tripBooking = new Trip();

        public int numberOfTravellers;
        public double cost;


        public Booking()
        {

        }

        public Booking(Customer c1, Trip t1, int n)
        {
            this.bookingCustomer = c1;
            this.tripBooking = t1;
            this.numberOfTravellers = n;
        }

        public double Cost
        {
            get { return cost; }
            set
            {
                if (numberOfTravellers > 5)
                {
                    cost = value * 0.95;
                }
                else
                {
                    cost = value;
                }
            }
        }

    }

    public class TravelAgency
    {

        public string name;
        public List<Customer> customers = new List<Customer>();
        public List<Tour> tours = new List<Tour>();
        public List<Trip> trips = new List<Trip>();
        public List<Booking> bookings = new List<Booking>();


        public TravelAgency()
        {

        }

        /*
        public TravelAgency (string travelAgencyName, Customer tTC, Tour tAT, Trip tATrip)
        {
            this.name = travelAgencyName;
            customers.Add(tTC);
            tours.Add(tAT);
            trips.Add(tATrip);
        }*/

        public TravelAgency(string travelAgencyName)
        {
            this.name = travelAgencyName;
        }

        public void Add(Customer customer)
        {
            customers.Add(customer);
        }

        public void Add(Tour tour)
        {
            tours.Add(tour);
        }

        public void Add(Trip trip)
        {
            trips.Add(trip);
        }

        public Tour FindTour(string city)
        {
            Tour item = tours.Find(x => x.Equals(city));

            return item;
        }

        public Trip FindTrip(string city)
        {
            Trip item = trips.Find(x => x.Equals(city));
            return item;
        }

        public Customer FindCustomer(string name)
        {
            Customer item = customers.Find(x => x.Equals(name));
            return item;
        }

        public void MakeBooking(Booking bk)
        {
            bookings.Add(bk);
        }

        public void ListTours()
        {
            Console.WriteLine("\nPrinting list of Upcoming Tours ... ...");
            for (int s = 0; s < tours.Count; s++)
            {
                Console.WriteLine($"Tour City = {tours[s].tourName}, Tour Cost = $ {tours[s].tourCost}, Tour Duration = {tours[s].tourDuration} nights");
            }
        }

        public void ListTrips()
        {
            Console.WriteLine("\nPrinting list of Available Trips ... ...");
            for (int i = 0; i < trips.Count; i++)
            {
                Console.WriteLine($"Trip City Location #{i + 1} = {tours[i].TourName}, Time = {trips[i].when}, Maximum Travellers for Trip = {trips[i].maximumSize} ");
            }
        }

        public void PrintCustomers()
        {
            Console.WriteLine("Printing list of current customers ... ...");
            for (int k = 0; k < customers.Count; k++)
            {
                Console.WriteLine("Customer Id = {0}, Customer Name = {1}, Customer Address = {2}", customers[k].ID, customers[k].CustomerName, customers[k].CustomerAddress);
            }
        }
    }



}
