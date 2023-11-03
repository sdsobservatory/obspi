import { AppBar, IconButton, Toolbar, SxProps, Theme, Typography } from "@mui/material";
import { MenuTwoTone } from "@mui/icons-material";

interface AppHeaderProps {
    handleToggle: () => void,
}

function AppHeader(props: AppHeaderProps) {
    return (
        <AppBar position="sticky" sx={styles.appBar}>
            <Toolbar>
                <IconButton onClick={() => props.handleToggle()} color="default">
                    <MenuTwoTone />
                </IconButton>
                {/* <Box component="img" sx={styles.appLogo} src="/src/assets/react.svg" /> */}
                <Typography sx={styles.apptitle}>SDSO</Typography>
            </Toolbar>
        </AppBar>
    );
}

const styles: Record<string, SxProps<Theme>> = {
    appBar: {},
    appLogo: {
        pl: 1,
    },
    apptitle: {
        pl: 2,
        fontSize: 24,
    }
};

export default AppHeader;