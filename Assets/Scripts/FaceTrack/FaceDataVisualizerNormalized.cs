using UnityEngine;

/// <summary>
/// Visualiza los puntos normalizados del rostro recibidos desde FaceDataReceiver,
/// centrados en el transform.position y con escala uniforme (independiente de la distancia a cámara).
/// </summary>
[ExecuteAlways]
public class FaceDataVisualizerNormalized : MonoBehaviour
{
    [Header("Referencia al receptor de datos")]
    public FaceDataReceiver receiver;

    [Header("Visualización")]
    [Range(0.001f, 0.02f)]
    public float radioPunto = 0.005f;

    [Range(0.1f, 3f)]
    public float escala = 1f; // escala visual del rostro normalizado

    public Color colorPuntos = Color.cyan;
    public bool mostrarTodos = false;
    public bool mostrarOjos = true;
    public bool mostrarCejas = true;
    public bool mostrarBoca = true;

    private void OnDrawGizmos()
    {
        if (receiver == null) return;
        if (receiver.puntosNormalizados == null || receiver.puntosNormalizados.Length == 0) return;

        Vector3 centro = transform.position;

        // Dibujar todos los puntos normalizados
        if (mostrarTodos)
        {
            Gizmos.color = colorPuntos;
            DibujarGrupo(receiver.puntosNormalizados, centro, escala, radioPunto);
        }

        // Ojos
        if (mostrarOjos)
        {
            Gizmos.color = Color.green;
            DibujarGrupo(receiver.ojoIzquierdoNormalizado, centro, escala, radioPunto);
            DibujarGrupo(receiver.ojoDerechoNormalizado, centro, escala, radioPunto);
        }

        // Cejas
        if (mostrarCejas)
        {
            Gizmos.color = Color.yellow;
            DibujarGrupo(receiver.cejaIzquierdaNormalizada, centro, escala, radioPunto);
            DibujarGrupo(receiver.cejaDerechaNormalizada, centro, escala, radioPunto);
        }

        // Boca
        if (mostrarBoca)
        {
            Gizmos.color = Color.red;
            DibujarGrupo(receiver.bocaNormalizada, centro, escala, radioPunto);
        }

        // Punto central (opcional para depurar)
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(centro, 0.005f);
    }

    private void DibujarGrupo(Vector3[] grupo, Vector3 centro, float escala, float radio)
    {
        if (grupo == null) return;
        foreach (var p in grupo)
        {
            // Ya vienen normalizados e invertidos en Y desde FaceDataReceiver
            Vector3 pos = p * escala;
            Gizmos.DrawSphere(centro + pos, radio);
        }
    }
}
