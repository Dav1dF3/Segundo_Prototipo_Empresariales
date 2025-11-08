// === BUSCARFUNDAS.JS ===

document.getElementById("btn-search").addEventListener("click", async () => {
  const codigoGuitarra = document.getElementById("input-guitar-code").value.trim();
  const codigoFunda = document.getElementById("input-funda-code").value.trim();

  if (!codigoGuitarra || !codigoFunda) {
    alert("Por favor ingresa un código de guitarra y un código de funda para buscar");
    limpiarCampos();
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

function limpiarCampos() {
  document.getElementById("input-guitar-code").value = "";
  document.getElementById("input-funda-code").value = "";
  document.getElementById("input-name").value = "";
  document.getElementById("input-price").value = "";
}