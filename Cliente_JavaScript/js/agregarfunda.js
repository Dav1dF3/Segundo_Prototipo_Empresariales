// === AGREGARFUNDA.JS ===

// Al cargar la página
document.addEventListener("DOMContentLoaded", () => {
  const btnAdd = document.getElementById("btn-add-case");
  btnAdd.addEventListener("click", agregarFunda);
});

// Agregar funda
async function agregarFunda() {

  // Código de la guitarra a la que se le agrega la funda
  const codigoGuitarra = document.getElementById("input-guitar-code").value.trim();

  // Datos básicos de la funda
  const funda = {
    codigo: document.getElementById("input-funda-code").value.trim(),
    nombre: document.getElementById("input-name-case").value.trim(), // opcional
    precio: parseFloat(document.getElementById("input-price-case").value) || 0
  };

  // ✅ Validar que los campos obligatorios estén completos (excepto nombre)
  if (
    !codigoGuitarra ||
    !funda.codigo ||
    !funda.precio
  ) {
    alert("⚠️ Por favor completa todos los campos obligatorios.");
    return;
  }

  console.log("JSON funda listo para API:", funda);

  // Llamar al backend
  try {
    const response = await fetch(`${BASE_URL}/guitarras/${codigoGuitarra}/fundas`, {
      method: "POST",
      headers: headers, // definidos en config.js
      body: JSON.stringify([funda]) // se envía como lista
    });

    if (response.status === 409) {
      alert("❌ El código ya existe. No puedes registrar un duplicado.");
      return;
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
    document.getElementById("input-guitar-code").value = "";
    document.getElementById("input-funda-code").value = "";
    document.getElementById("input-name-case").value = "";
    document.getElementById("input-price-case").value = "";
  }
}
