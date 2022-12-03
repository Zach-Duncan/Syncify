import format from "date-fns/format";
import getDay from "date-fns/getDay";
import parse from "date-fns/parse";
import startOfWeek from "date-fns/startOfWeek";
import React from "react";
import { Calendar as BigCalendar, dateFnsLocalizer } from "react-big-calendar";
import "react-big-calendar/lib/css/react-big-calendar.css";
import "react-datepicker/dist/react-datepicker.css";
import { EventGetDto } from "../../constants/types";
import "./calendar.css";

const locales = {
  "en-US": require("date-fns/locale/en-US"),
};
const localizer = dateFnsLocalizer({
  format,
  parse,
  startOfWeek,
  getDay,
  locales,
});

type BigCalendarEvent = {
  title: string;
  start: Date;
  end: Date;
};

const Calendar = ({ events }: { events: EventGetDto[] }) => {
  console.log(events);

  const calendarEvents = events.reduce((prev, curr) => {
    prev.push({
      title: curr.name,
      start: curr.startDate,
      end: curr.endDate,
    });
    return prev;
  }, [] as BigCalendarEvent[]);

  return (
    <BigCalendar
      localizer={localizer}
      events={calendarEvents}
      startAccessor="start"
      endAccessor="end"
      style={{ height: 500, margin: "50px" }}
    />
  );
};

export default Calendar;
