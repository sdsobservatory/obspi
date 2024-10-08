import { useEffect, useState } from "react"
import { Card, CardHeader, CardMedia } from "@mui/material"


interface AllSkyProps {
    name: string,
    url: string,
    hideHeader?: boolean,
    aspectRatio?: number,
}

function AllSkyComponent(props: AllSkyProps) {
    const [imgSrc, setImgSrc] = useState(props.url);

    const reloadImg = () => {
        setImgSrc(`${props.url}?t=${new Date().getTime()}`);
    };

    useEffect(() => {
        const intervalId = setInterval(reloadImg, 60000);
        return () => clearInterval(intervalId);
    });

    return (
        <Card>
            {(props.hideHeader !== undefined && props.hideHeader)
                ? null
                : <CardHeader title={props.name} />
            }
            <CardMedia
                sx={{ aspectRatio: props.aspectRatio ?? 1/1 }}
                component="img"
                alt="All Sky"
                image={imgSrc} />
        </Card>
    );
}

export default AllSkyComponent;