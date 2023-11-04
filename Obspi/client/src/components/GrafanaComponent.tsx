import { Card, CardMedia } from "@mui/material"

interface GrafanaProps {
    url: string,
    width?: string,
    height?: string,
}

function GrafanaComponent(props: GrafanaProps) {
    return (
        <Card>
            <CardMedia
                src={props.url}
                sx={{
                    border: "0px",
                    pointerEvents: "none",
                    height: props.height ?? "150px",
                    width: props.width ??  "150px",
                }}
                component="iframe"
            />
        </Card>
    );
}

export default GrafanaComponent;