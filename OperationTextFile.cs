namespace CQGTesting;

/// <summary>
/// Операции над текстовыми файлами 
/// </summary>
public class OperationTextFile
{
    /// <summary>
    /// Запись в словарь данных из конфигуриционного файла.
    /// </summary>
    /// <param name="path">Имя файла.</param>
    /// <returns>Словарь с данными.</returns>
    private static Dictionary<string, string> ReadConfigurationFile(string path)
    {
        var configDictionary = new Dictionary<string, string>();

        using var streamReader = new StreamReader(path);

        while (!streamReader.EndOfStream)
        {
            var configLine = streamReader.ReadLine();

            if (configLine is not null)
            {
                var configPair = configLine.Split('=');

                configDictionary.Add(configPair[0], configPair[1]);
            }

        }

        return configDictionary;
    }

    /// <summary>
    /// Запись в словарь данных из файла с текстом.
    /// </summary>
    /// <param name="path">Имя файла</param>
    /// <returns>Словарь с текстом и нулевым количеством изменений в нём.</returns>
    private static Dictionary<string, int> ReadSampleTextFile(string path)
    {
        var textDictionary = new Dictionary<string, int>();
        var countChanged = 0;

        using var streamReader = new StreamReader(path);

        while (!streamReader.EndOfStream)
        {
            var sampleLine = streamReader.ReadLine();

            if (sampleLine is not null)
            {
                textDictionary.Add(sampleLine, countChanged);
            }
        }

        return textDictionary;
    }

    /// <summary>
    /// Изменение текста из примерочного файла и количество изменений в нём.
    /// </summary>
    /// <param name="pathConfig">Имя конфигурационного файла.</param>
    /// <param name="pathSample">Имя файла с текстом.</param>
    /// <returns>Словарь с изменённым текстом и количеством изменений в нём.</returns>
    private static Dictionary<string, int> ChangeValueText(string pathConfig, string pathSample)
    {
        var configDictionary = ReadConfigurationFile(pathConfig);
        var sampleDictionary = ReadSampleTextFile(pathSample);
        var changedSampleText = new Dictionary<string, int>();
        var countChange = 0;

        foreach (var itemSample in sampleDictionary)
        {
            var sampleText = itemSample.Key;

            foreach (var itemConfig in configDictionary)
            {
                sampleText = sampleText.Replace(itemConfig.Key, itemConfig.Value);

                countChange = itemSample.Key
                                        .Zip(sampleText, (charSampleText, charChangedSampleText) =>
                                             charSampleText == charChangedSampleText ? 0 : 1)
                                        .Sum();
            }

            changedSampleText.Add(sampleText, countChange);
        }

        return changedSampleText;
    }

    /// <summary>
    /// Сортировка текста по количеству изменений в нём.
    /// </summary>
    /// <param name="pathConfig">Имя конфигурационного файла</param>
    /// <param name="pathSample">Имя файла с текстом.</param>
    /// <returns>Словарь с отсортированным текстом и количеством изменений по убыванию.</returns>
    public static Dictionary<string, int> SortedSampleTextChange(string pathConfig, string pathSample)
    {
        var changedSampleText = ChangeValueText(pathConfig, pathSample);

        if (string.IsNullOrWhiteSpace(pathSample) || string.IsNullOrWhiteSpace(pathConfig))
        {
            return changedSampleText;
        }

        return changedSampleText.OrderByDescending(x => x.Value)
                                .ToDictionary(x => x.Key, x => x.Value);
    }
}
