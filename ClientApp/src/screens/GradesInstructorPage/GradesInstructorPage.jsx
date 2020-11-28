import React from "react";
import { Container, Row, Col, Card, Form, Button } from "react-bootstrap";
import { withRouter } from "react-router";
import SideBar from "../../components/shared/SideBar/SideBar";
import GradesInstructor from "../../components/Instructor/GradesInstructor/GradesInstructor";
import styles from "./GradesInstructorPage.module.css";

const GradesInstructorPage = () => {
  return (
    <React.Fragment>
      <Container fluid>
        <Row>
          <Col xs={2}>
            <SideBar />
          </Col>
          {/* <Col xs={10} className={styles.pageContentWrapper}> */}
          <Col xs={10}>
            <GradesInstructor />
          </Col>
        </Row>
      </Container>
    </React.Fragment>
  );
};

export default GradesInstructorPage;
