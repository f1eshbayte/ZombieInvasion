using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private List<Task> _tasks;

    public event UnityAction OnAllTaskCompleted;
    
    private void OnEnable()
    {
        foreach (var task in _tasks)
        {
            task.OnTaskCompleted += CheckAllTasks;
        }
    }

    private void OnDisable()
    {
        foreach (var task in _tasks)
        {
            task.OnTaskCompleted -= CheckAllTasks;
        }
    }

    private void CheckAllTasks(Task completedTask)
    {
        if (_tasks.All(t => t.IsDone))
        {
            OnAllTaskCompleted?.Invoke();
            Debug.Log("Все задания выполнены! Запускаем следующую механику.");
        }
    }
}
