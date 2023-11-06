import { Routes, Route } from "react-router-dom";
import Dashboard from "../containers/Dashboard";
import Cameras from "../containers/Cameras";
import Observatory from "../containers/Observatory";
import Io from "../containers/Io";
import Diagnostics from "../containers/Diagnostics";
import Weather from "../containers/Weather";
import ErrorPage from "./ErrorPage";
import NoMatch from "./NoMatch";

function AppRoutes() {
    return (
        <Routes>
            <Route path="/" element={<Dashboard />} errorElement={<ErrorPage />} />
            <Route path="/cameras" element={<Cameras />} errorElement={<ErrorPage />} />
            <Route path="/observatory" element={<Observatory />} errorElement={<ErrorPage />} />
            <Route path="/io" element={<Io />} errorElement={<ErrorPage />} />
            <Route path="/diagnostics" element={<Diagnostics />} errorElement={<ErrorPage />} />
            <Route path="/weather" element={<Weather />} errorElement={<ErrorPage />} />
            <Route path="*" element={<NoMatch />} errorElement={<ErrorPage />} />
        </Routes>
    );
}

export default AppRoutes;