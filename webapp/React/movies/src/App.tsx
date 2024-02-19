
import { Outlet } from "react-router-dom";
import { Container } from "semantic-ui-react";

function App() {


    return (
        <>

            <Container className="container-style">
                <Outlet />
            </Container>

        </>
    )

}

export default App