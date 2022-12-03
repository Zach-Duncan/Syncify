import { Field, Form, Formik } from "formik";
import React, { useEffect, useState } from "react";
import { Input, Modal, Button, Dropdown } from "semantic-ui-react";
import {
  ApiResponse,
  EventCreateDto,
  EventGetDto,
  OptionDto,
} from "../../constants/types";
import axios from "axios";
import { BaseUrl } from "../../constants/env-cars";
import toast from "react-hot-toast";

const EventCreateModal = ({ refetchEvents }: { refetchEvents: () => {} }) => {
  const [firstOpen, setFirstOpen] = useState(false);
  const [secondOpen, setSecondOpen] = useState(false);
  const [calendarOptions, setCalendarOptions] = useState<OptionDto[]>();
  console.log("debug", calendarOptions);
  const initialValues: EventCreateDto = {
    calendarId: 0,
    name: "",
    eventDetails: "",
    startDate: new Date(),
    endDate: new Date(),
  };

  const onSubmit = async (values: EventCreateDto) => {
    const response = await axios.post<ApiResponse<EventGetDto>>(
      `${BaseUrl}/api/events`,
      values,
      {
        validateStatus: () => true,
      }
    );

    if (response.data.hasErrors) {
      toast.error("Error Occured, please try again", {
        position: "top-center",
        style: {
          background: "#333",
          color: "#fff",
        },
      });
      response.data.errors.forEach((err) => {
        console.log(err.message);
      });
    } else {
      setSecondOpen(true);
      toast.success("Event successfully created", {
        position: "top-center",
        style: {
          background: "#333",
          color: "#fff",
        },
      });
      refetchEvents();
    }
  };

  useEffect(() => {
    async function getCalendarOptions() {
      const response = await axios.get<ApiResponse<OptionDto[]>>(
        "/api/calendars/options"
      );

      setCalendarOptions(response.data.data);
    }

    getCalendarOptions();
  }, []);

  return (
    <>
      <Formik initialValues={initialValues} onSubmit={onSubmit}>
        <Modal
          as={Form}
          onClose={() => {
            setFirstOpen(false);
          }}
          onOpen={() => setFirstOpen(true)}
          open={firstOpen}
          trigger={
            <Button
              icon="circle plus"
              labelPosition="left"
              content="Event"
              positive
              onClick={() => setFirstOpen(true)}
            />
          }
        >
          <Modal.Header className="create-type-field">
            Create Event
          </Modal.Header>

          <Modal.Content>
            <Modal.Description>
              <div>
                <label htmlFor="name" className="field-title">
                  Event Title
                </label>
              </div>
              <Field id="name" name="name">
                {({ field }) => <Input {...field} />}
              </Field>
              <div>
                <label htmlFor="eventDetails" className="field-title">
                  Event Description
                </label>
              </div>
              <Field id="eventDetails" name="eventDetails">
                {({ field }) => <Input {...field} />}
              </Field>
              <div>
                <label htmlFor="startDate" className="field-title">
                  StartDate
                </label>
              </div>
              <Field id="startDate" name="startDate">
                {({ field }) => <Input type="date" {...field} />}
              </Field>

              <div>
                <label htmlFor="endDate" className="field-title">
                  EndDate
                </label>
              </div>
              <Field id="endDate" name="endDate">
                {({ field }) => <Input type="date" {...field} />}
              </Field>
              <div>
                <label htmlFor="calendar">Calendar</label>
              </div>
              <Field name="calendarId" id="calendarId" className="field">
                {({ field, form }) => (
                  <Dropdown
                    selection
                    options={calendarOptions}
                    {...field}
                    onChange={(_, { name, value }) =>
                      form.setFieldValue(name, value)
                    }
                    onBlur={(_, { name, value }) =>
                      form.setFieldValue(name, value)
                    }
                  />
                )}
              </Field>
            </Modal.Description>
          </Modal.Content>

          <Modal.Actions className="footer">
            <Button
              type="button"
              icon="cancel"
              content="Cancel"
              labelPosition="left"
              onClick={() => setFirstOpen(false)}
              negative
            />
            <Button
              type="submit"
              icon="calendar check"
              content="Create"
              labelPosition="left"
              positive
            />
          </Modal.Actions>
          <Modal
            onClose={() => setSecondOpen(false)}
            open={secondOpen}
            size="small"
          >
            <Modal.Header>Success!</Modal.Header>
            <Modal.Content>
              <p>You have successfully created an event in Syncify!</p>
            </Modal.Content>
            <Modal.Actions>
              <Button
                type="reset"
                icon="home"
                content="Home"
                labelPosition="right"
                positive
                onClick={() => {
                  setFirstOpen(false);
                }}
              />
            </Modal.Actions>
          </Modal>
        </Modal>
      </Formik>
    </>
  );
};

export default EventCreateModal;
