namespace shopping_system_project
{
    internal class Program
    {
        static public List <string> cardItems = new List<string>();
        static public Stack <string> action  = new Stack<string>();
        static public Dictionary <string , double> Products = new Dictionary<string, double>()
        {
            {"mobile" , 10000 },
            {"chair" ,100 },
            {"pen" , 10 }
        };
        static void Main(string[] args)
        {
            while(true)
            {
                Console.WriteLine("Welcome to shopping website");
                Console.WriteLine("1. Add item to cart");
                Console.WriteLine("2. View cart item");
                Console.WriteLine("3. Remove item from cart");
                Console.WriteLine("4. Chechout");
                Console.WriteLine("5. Undo last action");
                Console.WriteLine("6.Exit");

                Console.WriteLine("Enter your choice");
                int userChoice = int.Parse(Console.ReadLine());

                switch (userChoice)
                {
                    case 1:
                        Add();
                        break;

                    case 2:
                        View();
                        break;

                    case 3:
                        Remove();
                        break;

                    case 4:
                        checkout();
                        break;

                    case 5:
                        Undo();
                        break;

                    case 6:
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Please enter number between 1 to 6");
                        break;
                }
            }


        }

        private static void Remove()
        {
            View();
            Console.WriteLine("Enter the item you need to remove");
            string itemRemove = Console.ReadLine();

            if (cardItems.Contains(itemRemove))
            {
                cardItems.Remove(itemRemove);
                Console.WriteLine($"The {itemRemove} is removed");
                action.Push($"you Remove {itemRemove}");

            }
            else
            {
                Console.WriteLine("Item does not exist ");
            }
        }
        private static void Undo()
        {
           if(cardItems.Count > 0)
            {
                string lastAction = action.Pop();
                Console.WriteLine($"your last action is {lastAction}");

                string[] strings = lastAction.Split();

                if(strings.Contains("add"))
                {
                    cardItems.Remove(strings[2]);
                }
                else
                {
                    cardItems.Add(strings[2]);

                }
            }
        }

        private static void checkout()
        {
            if(cardItems.Any())
            {
                double totalPrice = 0;
                var totalitem = getItemPrice();

                foreach (var item in totalitem)
                {
                    totalPrice += item.Item2;

                }
                Console.WriteLine($"your price is {totalPrice}");
            }
            cardItems.Clear();
        }


        private static void View()
        {
            Console.WriteLine("your Products");
            
            if(cardItems.Any())
            {
                var itemPrice=getItemPrice();
                foreach (var item in itemPrice)
                {
                    Console.WriteLine($"The product {item.Item1} The price is {item.Item2} ");
                }
            }
            else
            {
                Console.WriteLine("you did not buy anything ");
            }
        }

        // Using IEnumerable retun type ,you can easily casting to any collections use
        // it is immutable you can not change its data "Security"
        private static IEnumerable<Tuple<string, double>> getItemPrice()
        {
            var ItemPrice = new List<Tuple<string, double>>();

            foreach (var item in cardItems)
            {
                double price = 0;
                bool isFound = Products.TryGetValue(item, out price);

                if(isFound)
                {
                    Tuple<string, double> proItem = new Tuple<string, double>(item, price);

                    ItemPrice.Add(proItem);

                }
            }
            return ItemPrice;
        }
        private static void Add()
        {
            Console.WriteLine("Items are: ");

            foreach(var item in Products)
            {
                Console.WriteLine($"The product name {item.Key} \t the price is {item.Value}");
            }
            Console.WriteLine("Enter product name");

            string cardItem = Console.ReadLine();

            if (Products.ContainsKey(cardItem))
            {
                cardItems.Add(cardItem);
                action.Push($"you add {cardItem}");
                Console.WriteLine($"Your poduct{cardItem} is added");
            }
            else
            {
                Console.WriteLine("The product dosen't found ");
            }
        }
    }
}