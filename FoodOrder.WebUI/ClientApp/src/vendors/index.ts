import "bootstrap/dist/css/bootstrap.css";
import moment from "moment";
import "moment/locale/ru";
import { extendMoment } from "moment-range";

moment.locale("ru");

// @ts-ignore
const extendedMoment = extendMoment(moment);

export { extendedMoment as moment };
