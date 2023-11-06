import { CameraAlt, Dashboard, Home, InfoOutlined, Tune, Thermostat } from "@mui/icons-material";
import { Sidebar, Menu, MenuItem } from "react-pro-sidebar";
import theme from "../theme";
import { Link, useLocation } from "react-router-dom";

interface SideNavProps {
    collapsed?: boolean,
    toggled?: boolean,
    broken?: boolean,
    onBreakPoint?: (broken: boolean) => void,
    setToggled?: (toggled: boolean) => void,
}

function SideNav(props: SideNavProps): JSX.Element {
    const location = useLocation();

    const toggleIfBroken = () => {
        if (props.broken && props.toggled) {
            if (typeof(props.setToggled) !== 'undefined') {
                props.setToggled(false);
            }
        }
    }

    return (
        <Sidebar
            collapsed={props.collapsed}
            toggled={props.toggled}
            onBreakPoint={props.onBreakPoint}
            onBackdropClick={toggleIfBroken}
            transitionDuration={100}
            breakPoint="sm"
            backgroundColor="#1e1e1e"
            style={{
                height: "100vh",
                top: "auto",
            }}>
            <Menu menuItemStyles={{
                button: ({ active }) => {
                    return {
                        backgroundColor: active ? theme.palette.primary.main : undefined,
                        '&:hover': {
                            backgroundColor: theme.palette.primary.dark,
                        }
                    }
                },
            }}>
                <MenuItem
                    active={location.pathname === "/"}
                    component={<Link to="/" />}
                    icon={<Dashboard />}
                    onClick={() => toggleIfBroken()}>
                    Dashboard
                </MenuItem>
                <MenuItem
                    active={location.pathname === "/cameras"}
                    component={<Link to="/cameras" />}
                    icon={<CameraAlt />}
                    onClick={() => toggleIfBroken()}>
                    Cameras
                </MenuItem>
                <MenuItem
                    active={location.pathname === "/observatory"}
                    component={<Link to="/observatory" />}
                    icon={<Home />}
                    onClick={() => toggleIfBroken()}>
                    Observatory
                </MenuItem>
                <MenuItem
                    active={location.pathname === "/io"}
                    component={<Link to="/io" />}
                    icon={<Tune />}
                    onClick={() => toggleIfBroken()}>
                    IO
                </MenuItem>
                <MenuItem
                    active={location.pathname === "/diagnostics"}
                    component={<Link to="/diagnostics" />}
                    icon={<InfoOutlined />}
                    onClick={() => toggleIfBroken()}>
                    Diagnostics
                </MenuItem>
                <MenuItem
                    active={location.pathname === "/weather"}
                    component={<Link to="/weather" />}
                    icon={<Thermostat />}
                    onClick={() => toggleIfBroken()}>
                    Weather
                </MenuItem>
            </Menu>
        </Sidebar>
    );
}

export default SideNav;