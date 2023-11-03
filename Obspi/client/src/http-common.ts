import axios from "axios";
import { OBSPI_API_URL } from "./constants";

export default axios.create({
    baseURL: OBSPI_API_URL,
    headers: {
        "Content-Type": "application/json",
    }
});
