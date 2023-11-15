import { Alert, Button, Snackbar } from "@mui/material"
import React from "react"

interface CommandButtonProps {
    buttonText: string,
    color?: undefined | "primary" | "secondary" | "error" ,
    handleClick: () => Promise<void>,
    isDisabled?: boolean,
}

function CommandButton(props: CommandButtonProps) {
    const [disabled, setDisabled] = React.useState(false);
    const [openSnackbar, setOpenSnackbar] = React.useState(false);

    const handleClose = (_: React.SyntheticEvent | Event, reason?: string) => {
        if (reason === "clickaway") {
            return;
        }

        setOpenSnackbar(false);
    };

    const handleClick = async () => {
        try {
            setDisabled(true);
            await props.handleClick();
        } catch (error) {
            setOpenSnackbar(true);
            console.error(error);
        } finally {
            setDisabled(false);
        }
    };

    React.useEffect(() => {
        setDisabled(props.isDisabled ?? false);
    }, [props.isDisabled])

    return (
        <>
            <Button
                color={props.color}
                variant="contained"
                disabled={disabled}
                onClick={() => handleClick()}>
                {props.buttonText}
            </Button>
            <Snackbar
                open={openSnackbar}
                autoHideDuration={6000}
                onClose={handleClose}>
                <Alert
                    onClose={handleClose}
                    severity="error"
                    variant="filled">
                    {props.buttonText} command failed, check log for details.
                </Alert>
            </Snackbar>
        </>
    )
}

export default CommandButton