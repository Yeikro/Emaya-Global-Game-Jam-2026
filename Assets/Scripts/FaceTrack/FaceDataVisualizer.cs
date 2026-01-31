using UnityEngine;

/// <summary>
/// Visualiza los puntos del rostro recibidos desde FaceDataReceiver usando Gizmos,
/// centrando la posición en el transform del GameObject.
/// </summary>
[ExecuteAlways]
public class FaceDataVisualizer : MonoBehaviour
{
    [Header("Referencia al receptor de datos")]
    public FaceDataReceiver receiver;

    [Header("Visualización")]
    [Range(0.001f, 0.02f)]
    public float radioPunto = 0.005f;

    [Range(0.1f, 3f)]
    public float escala = 1f;  // escala general del rostro

    public Color colorPuntos = Color.cyan;
    public bool mostrarTodos = false;
    public bool mostrarOjos = true;
    public bool mostrarCejas = true;
    public bool mostrarBoca = true;

    private void OnDrawGizmos()
    {
        if (receiver == null) return;
        if (receiver.puntos == null || receiver.puntos.Length == 0) return;

        Vector3 centro = transform.position;

        // Dibujar todos los puntos si está activo
        if (mostrarTodos)
        {
            Gizmos.color = colorPuntos;
            DibujarGrupo(receiver.puntos, centro, escala, radioPunto);
        }

        // Ojos
        if (mostrarOjos)
        {
            Gizmos.color = Color.green;
            DibujarGrupo(receiver.ojoIzquierdo, centro, escala, radioPunto);
            DibujarGrupo(receiver.ojoDerecho, centro, escala, radioPunto);
        }

        // Cejas
        if (mostrarCejas)
        {
            Gizmos.color = Color.yellow;
            DibujarGrupo(receiver.cejaIzquierda, centro, escala, radioPunto);
            DibujarGrupo(receiver.cejaDerecha, centro, escala, radioPunto);
        }

        // Boca
        if (mostrarBoca)
        {
            Gizmos.color = Color.red;
            DibujarGrupo(receiver.boca, centro, escala, radioPunto);
        }
    }

    private void DibujarGrupo(Vector3[] grupo, Vector3 centro, float escala, float radio)
    {
        if (grupo == null) return;
        foreach (var p in grupo)
        {
            // Invertir eje Y (de MediaPipe) y centrar respecto al transform
            Vector3 pos = new Vector3(
                p.x * escala,
                (1f - p.y) * escala,  // invertir Y
                p.z * escala
            );

            // Centrar en la posición del GameObject
            Gizmos.DrawSphere(centro + pos, radio);
        }
    }
}
