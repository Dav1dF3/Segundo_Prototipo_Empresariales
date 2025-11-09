package com.unibague.poctiendainstrumentos.repository;

import com.unibague.poctiendainstrumentos.dto.FiltroFundaDTO;
import com.unibague.poctiendainstrumentos.model.Funda;
import jakarta.persistence.EntityManager;
import jakarta.persistence.PersistenceContext;
import jakarta.persistence.TypedQuery;

import java.util.List;

public class FundaRepositoryImpl implements FundaRepositoryCustom {

    @PersistenceContext
    private EntityManager entityManager;

    @Override
    public List<Funda> filtrarFundas(FiltroFundaDTO filtro) {
        StringBuilder jpql = new StringBuilder("SELECT f FROM Funda f WHERE 1=1 ");

        if (filtro.getNombre() != null && !filtro.getNombre().isEmpty()) {
            jpql.append("AND LOWER(f.nombre) LIKE LOWER(CONCAT('%', :nombre, '%')) ");
        }
        if (filtro.getPrecioMin() != null) {
            jpql.append("AND f.precio >= :precioMin ");
        }
        if (filtro.getPrecioMax() != null) {
            jpql.append("AND f.precio <= :precioMax ");
        }
        if (filtro.getCodigoGuitarra() != null) {
            jpql.append("AND f.codigo_guitarra = :codigoGuitarra ");
        }

        TypedQuery<Funda> query = entityManager.createQuery(jpql.toString(), Funda.class);

        if (filtro.getNombre() != null && !filtro.getNombre().isEmpty()) {
            query.setParameter("nombre", filtro.getNombre());
        }
        if (filtro.getPrecioMin() != null) {
            query.setParameter("precioMin", filtro.getPrecioMin());
        }
        if (filtro.getPrecioMax() != null) {
            query.setParameter("precioMax", filtro.getPrecioMax());
        }
        if (filtro.getCodigoGuitarra() != null) {
            query.setParameter("codigoGuitarra", filtro.getCodigoGuitarra());
        }

        return query.getResultList();
    }
}
