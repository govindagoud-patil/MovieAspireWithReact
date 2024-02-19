import apiConnector from "../../api/apiConnector";
import { MovieDto } from "../../models/movieDto"
import React, { useEffect, useState } from "react"
import { Button, Container, Dropdown, Pagination } from "semantic-ui-react";
import MovieTableItems from "./MovieTableItem";
import { NavLink } from "react-router-dom";
import authSvc from "../../auth/authSvc";
import { setPgRequestParams } from '../../feature/pagination/paginationSlice'
import { useAppDispatch, useAppSelector } from '../../redux/hooks'
export default function MovieTable() {


    const selectPgRequestParam = useAppSelector(state => state.pagination.paginationRequestParam);
    const dispatch = useAppDispatch();
    const [movies, setMovies] = useState<MovieDto[]>([]);
    const [pageSize, setPageSize] = useState(5);
    const [pageNumber, setPageNumber] = useState(0);
    const [totalPageNumber, setTotalPageNumber] = useState(0);


    const fetchDate = async () => {
        const paginationResults = await apiConnector.getPaginatedMovies(selectPgRequestParam);
        if (paginationResults.data) {
            const { data, paginationParams } = paginationResults;
            if (data && paginationParams) {
                setMovies(data);
                setTotalPageNumber(paginationParams.totalPages);
                setPageNumber(paginationParams.currentPage);
                setPageSize(paginationParams.pagesize);
            }

        }

    }

    useEffect(() => {
        fetchDate();
    }, [selectPgRequestParam.pageNumber, selectPgRequestParam.pageSize]);

    const handlePageSizeChange = (_: React.SyntheticEvent, data: any) => {
        dispatch(setPgRequestParams({ ...selectPgRequestParam, pageSize: data.value }))
    }

    const handlePageNumberChange = (_: React.MouseEvent<HTMLAnchorElement>, data: any) => {
        dispatch(setPgRequestParams({ ...selectPgRequestParam, pageNumber: data.activePage }))
    }


    const options = [
        { key: 5, text: '5', value: 5 },
        { key: 10, text: '10', value: 10 },
        { key: 20, text: '20', value: 20 },
        { key: 30, text: '30', value: 30 },
    ];

    return (
        <>
            <Container className="container-style" >

                <table className="ui inverted table">
                    <thead style={{ textAlign: 'center' }}>
                        <tr>
                            <th>Id</th>
                            <th>Title</th>
                            <th>Description</th>
                            <th>CreateDate</th>
                            <th>Category</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        {movies.length !== 0 && (
                            movies.map((movie, index) => (
                                <MovieTableItems key={index} movie={movie} />
                            ))
                        )}
                    </tbody>
                </table>
                <div style={{ marginBottom: '20px', marginTop: '20px', display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                    <Dropdown selection options={options} value={pageSize} onChange={handlePageSizeChange}
                    />
                    <Pagination activePage={pageNumber} totalPages={totalPageNumber} onPageChange={handlePageNumberChange} />
                </div>

                <Button as={NavLink} to="createMovie" floated="right" type="button" content="Create Movie" positive />
                <Button as={NavLink} to={`/`} type="submit" color="orange" onClick={async () => authSvc.logOut()} >Logout</Button>

            </Container>
        </>
    )
}