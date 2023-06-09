﻿namespace api.ViewModel
{
    public class ResultViewModel<TModel>
    {

        public TModel Data { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();

        public ResultViewModel(TModel data, List<string> errors)
        {
            Data = data;
            Errors = errors;

        }

        public ResultViewModel(TModel data) { 
            Data = data;
        }

        public ResultViewModel(List<string> errors) { 
            Errors = errors;
        }

        public ResultViewModel(string error) {
            Errors.Add(error);
        }


    }
}
