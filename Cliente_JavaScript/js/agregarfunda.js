// === AGREGARFUNDA.JS ===

// Al cargar la página
document.addEventListener("DOMContentLoaded", () => {
  const btnAdd = document.getElementById("btn-add-case");
  btnAdd.addEventListener("click", agregarFunda);
});

// Agregar funda
async function agregarFunda() {

  // Código de la guitarra a la que se le agrega la funda
  // desde el input del formulario
  // Ejemplo: si la guitarra tiene código "GTR001", la URL será .../guitarras/GTR001/fundas
  const codigo = document.getElementById("input-guitar-code").value.trim();

  // Datos básicos de la funda
  const funda = {
      codigo: document.getElementById("input-funda-code").value.trim(),
      nombre: document.getElementById("input-name-case").value.trim(),
      precio: parseFloat(document.getElementById("input-price-case").value) || 0
    };

  console.log("JSON funda listo para API:", funda);

  // Llamar al backend
  try {
    const response = await fetch(`${BASE_URL}/guitarras/${codigo}/fundas`, {
      method: "POST",
      headers: headers, // definidos en config.js
      body: JSON.stringify(funda)
    });

    if (response.status === 409) {
      alert("❌ El código ya existe. No puedes registrar un duplicado.");
      return; // detenemos aquí para no continuar con la lógica de éxito
    }

    if (!response.ok) {
      throw new Error("Error al guardar la funda");
    }

    alert("✅ Funda agregada correctamente");
    limpiarCampos();

  } catch (error) {
    console.error("Error:", error);
    alert("Hubo un error al agregar la funda ❌");
  }

  // =====================
  // Limpiar campos del formulario de funda
  // =====================
  function limpiarCampos() {
    // Campos de la funda
    document.getElementById("input-guitar-code").value = "";
    document.getElementById("input-funda-code").value = "";
    document.getElementById("input-name-case").value = "";
    document.getElementById("input-price-case").value = "";
  }

}
