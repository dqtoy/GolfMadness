using System.Collections.Generic;
using UnityEngine;

public class UIStackController : UniqueElement
{
    public enum Type
    {
        SCREEN,
        POPUP
    }

    Stack<UIController> _screenList = new Stack<UIController>();
    Stack<UIController> _popupList = new Stack<UIController>();

    [SerializeField] private GameObject _screenParent;
    [SerializeField] private GameObject _popupParent;

    #region Screen methods

    public void Push(UIController screen)
    {
        if (screen.Type == Type.POPUP)
        {
            Push(_popupList, screen);
        }
        else
        {
            Push(_screenList, screen);
        }
    }

    #endregion

    public void PopAll()
    {
        while (_popupList.Count > 0)
        {
            Pop(_popupList.Peek());
        }
        
        while (_screenList.Count > 0)
        {
            Pop(_screenList.Peek());
        }
    }

    public void Pop(UIController screen)
    {
        if (_popupList.Count > 0 && _popupList.Peek() == screen)
        {
            Pop(_popupList);
        }
        else if (_screenList.Count > 0 && _screenList.Peek() == screen)
        {
            Pop(_screenList);
        }
    }

    protected void Pop(Stack<UIController> list)
    {
        var screen = list.Pop();
        screen.OnDisappeared();
        Destroy(screen.gameObject);
    }

    protected void Push(Stack<UIController> list, UIController screen)
    {
        if (list.Count > 0)
        {
            var previous = list.Peek();
            previous.OnDisappeared();
            previous.gameObject.SetActive(false);
        }

        list.Push(screen);
        screen.gameObject.SetActive(true);
        screen.OnAppeared();
    }

    public UIController Push(string path)
    {
        UIController controller = GetView(path);
        Push(controller);
        return controller;
    }

    public UIController GetView(string path)
    {
        var screen = Resources.Load<GameObject>(path);
        var instance = Instantiate(screen, GetParent(screen.GetComponent<UIController>().Type).transform);
        return instance.GetComponent<UIController>();
    }

    protected GameObject GetParent(Type type)
    {
        if (type == Type.POPUP)
        {
            return _popupParent;
        }

        return _screenParent;
    }
}