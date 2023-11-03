import { Container, Typography } from "@mui/material";
import { useRouteError, isRouteErrorResponse } from "react-router-dom"

function ErrorPage() {
    const error = useRouteError();
    console.error(error);

    return (
        <Container>
            <Typography variant="h2" align="center">Oops!</Typography>
            <Typography variant="body1" align="center">Sorry, an unexpected error has occurred.</Typography>
            <Typography variant="body2" align="center">
                <i>
                    {
                        isRouteErrorResponse(error)
                            ? (error.status || error.statusText)
                            : "Unknown error message"
                    }
                </i>
            </Typography>
        </Container>
    );
}

export default ErrorPage;