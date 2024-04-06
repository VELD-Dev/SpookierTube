namespace SpookierTubeLib.Utils;

public static class VisualElementUtils
{
    public class NoKidsFoundException : Exception
    {
        public NoKidsFoundException(string message) : base(message) { }
        public NoKidsFoundException(string message, Exception exception) : base(message, exception) { }
    }

    public static TElem GetChild<TElem>(this VisualElement visualElement, string name) where TElem : VisualElement
    {
        var child = visualElement.contentContainer.Children().First((ve) => ve.GetType() == typeof(TElem) && ve.name == name);

        if (child is not TElem elem || child is null)
            throw new NoKidsFoundException($"{name} cannot be found with type {typeof(TElem).FullName}.");

        return elem;
    }

    public static VisualElement GetChild(this VisualElement visualElement, string name, Type type)
    {
        var child = visualElement.contentContainer.Children().First((ve) => ve.GetType() == type && ve.name == name);

        if (child is null)
            throw new NoKidsFoundException($"{name} cannot be found with type {type.FullName}.");

        return child;
    }
}
