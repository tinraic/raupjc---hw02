using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_2
{
    public class TodoRepository : ITodoRepository
    {
        /// <summary >
        /// Repository  does  not  fetch  todoItems  from  the  actual  database ,
        /// it uses in  memory  storage  for  this  excersise.
        ///  </summary >
        private readonly IGenericList<TodoItem> _inMemoryTodoDatabase;

        public TodoRepository(IGenericList<TodoItem> initialDbState = null)
        {
            if (initialDbState != null)
            {
                _inMemoryTodoDatabase = initialDbState;
            }
            else
            {
                _inMemoryTodoDatabase = new GenericList<TodoItem>();
            }
            //  Shorter  way to  write  this in C# using ??  operator:
            // x ?? y => if x is not null , expression  returns x. Else it will return y.
            //  _inMemoryTodoDatabase = initialDbState  ?? new List <TodoItem >();

        }

        public TodoItem Get(Guid todoId) => _inMemoryTodoDatabase.Where(t => t.Id == todoId).FirstOrDefault();

        public TodoItem Add(TodoItem todoItem)
        {
            if (todoItem != null)
            {
                if (_inMemoryTodoDatabase.Contains(todoItem))
                {
                    throw new DuplicateTodoItemException("duplicate id = {todoItem.id}");
                }

                else
                {
                    _inMemoryTodoDatabase.Add(todoItem);
                    
                }

            }

            return todoItem;
        }

        public bool Remove(Guid todoId) =>_inMemoryTodoDatabase.Remove(Get(todoId));

        public TodoItem Update(TodoItem todoItem)
        {
            if (todoItem != null)
            {
                if (Get(todoItem.Id) == null)
                {
                    Add(todoItem);
                }

                else
                {
                    Remove(todoItem.Id);
                    Add(todoItem);
                }
            }

            return todoItem;

        }

        public bool MarkAsCompleted(Guid todoId) =>
            _inMemoryTodoDatabase.Where(t => t.Id == todoId).FirstOrDefault().MarkAsCompleted();

        public List<TodoItem> GetAll() => _inMemoryTodoDatabase.OrderByDescending(t => t.DateCreated).ToList();

        public List<TodoItem> GetActive() => _inMemoryTodoDatabase.Where(t => !t.IsCompleted).ToList();

        public List<TodoItem> GetCompleted() => _inMemoryTodoDatabase.Where(t => t.IsCompleted).ToList();

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction) =>
            _inMemoryTodoDatabase.Where(filterFunction).ToList();
    }

    public class DuplicateTodoItemException : Exception
    {
        public DuplicateTodoItemException(string duplicateIdTodoitemId)
        {
            Console.WriteLine(duplicateIdTodoitemId);
        }
    }
}