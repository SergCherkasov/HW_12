var path1 = "test.txt";
var path2 = "test2.txt";
var path3 = "test3.txt";
var path4 = "test4.txt";
var path5 = "test5.txt";


var task1 = Task.Run(() => ReadFile(path1));
var task2 = Task.Run(() => ReadFile(path2));
var task3 = Task.Run(() => ReadFile(path3));
var task4 = Task.Run(() => ReadFile(path4));
var task5 = Task.Run(() => ReadFile(path5));





await Task.WhenAll(task1, task2, task3, task4, task5);


Console.WriteLine($"\nAmount chars in {path1} {task1.Result}\n");
Console.WriteLine($"\nAmount chars in {path2} {task2.Result}\n");
Console.WriteLine($"\nAmount chars in {path3} {task3.Result}\n");
Console.WriteLine($"\nAmount chars in {path4} {task4.Result}\n");
Console.WriteLine($"\nAmount chars in {path5} {task5.Result}\n");



async Task<int> ReadFile(string path)
{

    var amountChar = 0;

    await Task.Run(async () =>
    {
        StreamReader file = new StreamReader(path);
        try
        {
            if (file.EndOfStream)
            {
                await Task.Run(() => throw new IOException());
            }

            char[] buffer = new char[50];

            while (!file.EndOfStream)
            {

                var charsRead = await file.ReadBlockAsync(buffer, 0, buffer.Length);
                amountChar += charsRead;
                foreach (var c in buffer)
                {
                    Console.Write(c);
                }

                await Task.Delay(1000);
            }
        }
        catch (IOException ioEx)
        {
            Console.WriteLine($"\n\n{ioEx.Message} in {path} \nFile is empty\n");
        }
    });
    return amountChar;
}