package com.kodgemisi.webapps.vuforia;

/*
 * 17.08.03 정다은 추가 
 * vuforia cloud recognization java example 참조
 * Reference: https://developer.vuforia.com/resources/dev-guide/adding-target-cloud-database-api
 */

public interface TargetStatusListener {

	public void OnTargetStatusUpdate(TargetState targetState);
}
