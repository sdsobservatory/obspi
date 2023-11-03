import { Card, CardMedia } from "@mui/material"

interface GrafanaProps {
    url: string,
}

function GrafanaComponent(props: GrafanaProps) {
    return (
        <Card>
            <CardMedia
                height="200px"
                width="200px"
                src={props.url}
                sx={{
                    border: "0px",
                    pointerEvents: "none",
                }}
                component="iframe"
            />
        </Card>
    );
}

export default GrafanaComponent;