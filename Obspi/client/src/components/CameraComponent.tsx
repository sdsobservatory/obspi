import { Card, CardHeader, CardMedia } from "@mui/material"

interface CameraProps {
    name: string,
    url: string,
    hideHeader?: boolean,
}

function CameraComponent(props: CameraProps) {
    return (
        <Card>
            {(props.hideHeader !== undefined && props.hideHeader)
                ? null
                : <CardHeader title={props.name} />
            }
            <CardMedia
                src={props.url}
                sx={{
                    aspectRatio: "16/9",
                    border: "0px",
                }}
                component="iframe"
                allowFullScreen
            />
        </Card>
    );
}

export default CameraComponent;