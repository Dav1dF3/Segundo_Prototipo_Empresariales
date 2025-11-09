// =====================
// Buscar funda por código
// =====================
document.getElementById("btn-search").addEventListener("click", async () => {
  const codigoGuitarra = document.getElementById("input-guitar-code").value.trim();
  const codigoFunda = document.getElementById("input-funda-code").value.trim();

  if (!codigoGuitarra || !codigoFunda) {
    alert("Por favor ingresa un código de guitarra y un código de funda para buscar");
    return;
  }

  try {
    const funda = await buscarFundas(codigoGuitarra, codigoFunda);

    if (!funda) {
      alert("No se encontró ninguna funda con ese código ❌");
      limpiarCampos();
      return;
    }

    document.getElementById("input-name").value = funda.nombre || "";
    document.getElementById("input-price").value = funda.precio || "";

  } catch (error) {
    console.error("Error:", error);
    alert("Hubo un error al buscar la funda ❌");
  }
});

// =====================
// Actualizar funda
// =====================
document.getElementById("btn-update").addEventListener("click", async () => {
  const codigoGuitarra = document.getElementById("input-guitar-code").value.trim();
  const codigoFunda = document.getElementById("input-funda-code").value.trim();

  if (!codigoGuitarra || !codigoFunda) {
    alert("Primero busca una funda antes de actualizar ⚠️");
    return;
  }

  const fundaModificada = {
    codigo: codigoFunda,
    nombre: document.getElementById("input-name").value.trim(),
    precio: parseFloat(document.getElementById("input-price").value) || 0
  };

  try {
    const response = await editarFunda(codigoGuitarra, codigoFunda, fundaModificada);

    if (response) {
      alert("Funda actualizada correctamente ✅");
      limpiarCampos();
    } else {
      alert("Hubo un problema al actualizar la funda ❌");
    }
  } catch (error) {
    console.error("Error:", error);

    if (error.message.includes("404")) {
      alert("La funda no existe (404)");
    } else if (error.message.includes("409")) {
      alert("Ya existe una funda con ese código (409)");
    } else {
      alert("Error inesperado al actualizar la funda ❌");
    }
  }
});

// =====================
// Limpiar campos
// =====================
function limpiarCampos() {
  document.getElementById("input-funda-code").value = "";
  document.getElementById("input-guitar-code").value = "";
  document.getElementById("input-name").value = "";
  document.getElementById("input-price").value = "";
}
