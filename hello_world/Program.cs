
// Write 3 times
for (int i = 0; i < 3; i++)
{
    Console.WriteLine("Hello, World!");
}

// While user has not entered "exit", keep asking for input every second
string? input = "";
while (input != "exit")
{
    Console.WriteLine("Enter 'exit' to quit");
    input = Console.ReadLine();
}