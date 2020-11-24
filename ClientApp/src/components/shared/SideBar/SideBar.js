import React from "react";
import { Nav } from "react-bootstrap";
import { withRouter } from "react-router";
import styles from "./SideBar.module.css";

const Side = (props) => {
  return (
    <>
      <Nav
        className={`${styles.sidebar} col-md-12 d-none d-md-block bg-light`}
        activeKey="/home"
        onSelect={(selectedKey) => alert(`selected ${selectedKey}`)}
      >
        <div className="sidebar-sticky"></div>
        <Nav.Item>
          <Nav.Link href="/home">Active</Nav.Link>
        </Nav.Item>
        <Nav.Item>
          <Nav.Link eventKey="link-1">Link</Nav.Link>
        </Nav.Item>
        <Nav.Item>
          <Nav.Link eventKey="link-2">Link</Nav.Link>
        </Nav.Item>
        <Nav.Item>
          <Nav.Link eventKey="disabled" disabled>
            Disabled
          </Nav.Link>
        </Nav.Item>
      </Nav>
    </>
  );
};
const SideBar = withRouter(Side);
export default SideBar;
