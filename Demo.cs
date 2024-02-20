namespace CQGTesting;

/// <summary>
/// Демонстрация программы.
/// </summary>
public static class Demo
{
    public static void StartProgram(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("\nНеверное число аргументов!");
            return;
        }

        var configFile = args[0];
        var sampleFile = args[1];

        try
        {
            var sortedSampleDictionary = OperationTextFile.SortedSampleTextChange(configFile, sampleFile);

            Console.WriteLine("\nОтсортированный по убыванию по измененниям текст и количество изменений в нём\n");

            foreach (var item in sortedSampleDictionary)
            {
                Console.WriteLine($"{item.Key}  :  {item.Value}");
            }
        }
        catch (Exception)
        {
            if (!File.Exists(configFile))
            {
                Console.WriteLine("Не найден введённый конфигурационный файл, проверьте правильность введенного названия!");
            }

            if (!File.Exists(sampleFile))
            {
                Console.WriteLine("Не найден введённый файл с примером текста, проверьте правильность введенного названия!");
            }

        }
    }
}
