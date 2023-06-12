using CallaciMaui.Helpers;
using CallaciMaui.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallaciMaui
{
    public class QuestionRepository
    {
        string _dbPath;

        public string StatusMessage { get; set; }

        private SQLiteAsyncConnection conn;

        private async Task Init()
        {
            if (conn != null)
                return;

            //var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData.db");
            //var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData");
            conn = new SQLiteAsyncConnection(_dbPath);
            await conn.CreateTableAsync<Question>();
        }

        public QuestionRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public async Task AddNewQuestion(string mark, string questionText, string solution, string image,
            string answers)
        {
            int result;
            try
            {
                await Init();

                // basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(mark) && string.IsNullOrEmpty(questionText) 
                    && string.IsNullOrEmpty(image)
                    && string.IsNullOrEmpty(solution) && string.IsNullOrEmpty(answers))
                    throw new Exception("One or more question's elements is missing");

                result = await conn.InsertAsync(new Question { 
                    Mark = mark,
                    QuestionText = questionText,
                    Solution = solution,
                    Image = image,
                    Answers = answers});

                StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, mark);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", mark, ex.Message);
            }

        }

        public async Task<List<Question>> GetAllQuestion()
        {
            // TODO: Init then retrieve a list of Question objects from the database into a list
            try
            {
                await Init();
                return await conn.Table<Question>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Question>();
        }

        public async Task removePeople(int id)
        {
            try
            {
                await Init();
                await conn.DeleteAsync<Question>(id);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to delete {0}. Error: {1}", id, ex.Message);
            }
        }
    }
}
