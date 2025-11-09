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
// Eliminar funda
// =====================
document.getElementById("btn-delete").addEventListener("click", async () => {
  const codigoGuitarra = document.getElementById("input-guitar-code").value.trim();
  const codigoFunda = document.getElementById("input-funda-code").value.trim();

  if (!codigoGuitarra || !codigoFunda) {
    alert("Primero busca una funda antes de eliminar ⚠️");
    return;
  }

  const confirmar = confirm(`¿Seguro que deseas eliminar la funda con código ${codigoFunda}? Esta acción no se puede deshacer.`);
  if (!confirmar) return;

  try {
    const response = await eliminarFunda(codigoGuitarra, codigoFunda);

    if (response) {
      alert("Funda eliminada correctamente ✅");
      limpiarCampos();
    } else {
      alert("Hubo un problema al eliminar la funda ❌");
    }
  } catch (error) {
    console.error("Error:", error);

    if (error.message.includes("404")) {
      alert("La funda no existe (404)");
    } else {
      alert("Error inesperado al eliminar la funda ❌");
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
