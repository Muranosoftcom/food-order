import "bootstrap/dist/css/bootstrap.min.css";
import moment from "moment";
import { extendMoment } from "moment-range";

// @ts-ignore
const extendedMoment = extendMoment(moment);

export { extendedMoment as moment };
