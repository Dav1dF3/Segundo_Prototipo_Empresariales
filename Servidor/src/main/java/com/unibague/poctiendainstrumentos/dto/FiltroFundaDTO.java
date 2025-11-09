package com.unibague.poctiendainstrumentos.dto;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Builder
public class FiltroFundaDTO {
    /**
     * Nombre parcial o completo de la funda.
     */
    private String nombre;

    /**
     * Precio mínimo de la funda.
     */
    private Double precioMin;

    /**
     * Precio máximo de la funda.
     */
    private Double precioMax;

    /**
     * Código de la guitarra asociada
     */
    private Long codigoGuitarra;

}
