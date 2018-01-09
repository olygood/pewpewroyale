using System.IO;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

public class DualBehaviour : MonoBehaviour
{
    #region Public Members

    #endregion

    #region Public void

    #endregion

    #region System

    protected virtual void Awake()
    {
        Init();
    }

    protected virtual void Reset()
    {
        Init();
    }

    #endregion

    #region Class Methods

    private void Init()
    {
        if (m_transform == null)
            m_transform = GetComponent<Transform>();
    }


    #endregion

    #region Tools Debug and Utility

#if UNITY_EDITOR
    [ContextMenu("Generate Editor for this script")]
    private void GenerateEditor_instance()
    {
        _GenerateEditor(this.GetType().Name);
    }

    [MenuItem("Assets/Generate Editor for this script", true)]
    private static bool GenerateEditor_validation()
    {
        // Will only show up this menu if the asset being clicked on is a script

        return Selection.activeObject is MonoScript;
    }

    [MenuItem("Assets/Generate Editor for this script")]
    private static void GenerateEditor_static()
    {
        _GenerateEditor(Selection.activeObject.name);
    }

    private static void _GenerateEditor(string _name)
	{
        string editorTemplateGUID = AssetDatabase.FindAssets(Path.GetFileNameWithoutExtension(ImportNinja.editorTemplate)).FirstOrDefault();

        string editorTemplatePath = AssetDatabase.GUIDToAssetPath(editorTemplateGUID);
        string editorTemplateFullPath = new DirectoryInfo(editorTemplatePath).FullName;

        File.WriteAllText(
            editorTemplateFullPath.Replace(ImportNinja.editorTemplate, _name + "Editor.cs"),
            File.ReadAllText(editorTemplateFullPath).Replace("#CLASSNAME#", _name)
        );

        AssetDatabase.Refresh();
    }
#endif

    #endregion

    #region Private and Protected Members

    [SerializeField]
    protected Transform m_transform;

    #endregion
}
