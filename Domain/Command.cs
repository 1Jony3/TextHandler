using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Command: ICommand
    {
        private Receiver _receiver;

        private string[] _filePaths;
        private string[] _outputDirectory;
        private int _minWordLength;
        private bool _removePunctuation;
        public Command(
            Receiver receiver, 
            string[] filePaths, 
            string[] outputDirectory, 
            int minWordLength,
            bool removePunctuation
            )
        {
            _receiver = receiver;
            this._filePaths = filePaths;
            this._outputDirectory = outputDirectory;
            this._minWordLength = minWordLength;
            this._removePunctuation = removePunctuation;
        }

        public async void Execute() => await this._receiver.ProcessFiles(
            _filePaths, 
            _outputDirectory, 
            _minWordLength, 
            _removePunctuation
            );

    }
}
