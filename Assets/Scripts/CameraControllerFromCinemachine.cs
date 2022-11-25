using UnityEngine;

/// <summary>
/// Cinemachine �p�̃J��������N���X
/// Follow �ɂ͒Ǐ]�Ώۂ̃Q�[���I�u�W�F�N�g���A�T�C���BLook At �̓A�T�C���Ȃ��B
/// Boby �ɂ� Framing Transposer ��ݒ�BGame �r���[�ŃJ�����̉ғ��͈�(Soft Zone Width �� Height)��ݒ�ł���悤�ɂȂ�
/// �J������ Z �ʒu�� Camera Distance �ɂĐݒ�
/// Aim �ɂ� DoAnything ��ݒ�B�����ݒ肵�Ȃ��ƃJ�������㉺��]���Ȃ�

/// </summary>
public class CameraControllerFromCinemachine : MonoBehaviour
{
    [Header("�ǐՂ���Q�[���I�u�W�F�N�g")]
    public GameObject targetObj;

    [SerializeField]
    private float cameraRotateSpeed = 80.0f;     // �J�����̉�]���x

    [SerializeField]
    private float maxLimit = 45.0f;              // X �������̍ő���͈�

    [SerializeField]
    private float minLimit = 25.0f;              // X �������̍ŏ����͈�


    void Update() {
        if (Input.GetMouseButton(1)) {
            // �J�����̉�]
            RotateCamera();
        }
    }

    /// <summary>
    /// targetObj �����ɂ����J�����̌��]��]
    /// </summary>
    private void RotateCamera() {

        // �}�E�X�̓��͒l���擾
        float x = Input.GetAxis("Mouse X");
        float z = Input.GetAxis("Mouse Y");

        // �J������Ǐ]�Ώۂ̎��͂����]��]������
        transform.RotateAround(targetObj.transform.position, Vector3.up, x * Time.deltaTime * cameraRotateSpeed);

        //�J�����̉�]���̏����l���Z�b�g
        var localAngle = transform.localEulerAngles;

        // X ���̉�]�����Z�b�g
        localAngle.x += z;

        // X �����ғ��͈͓��Ɏ��܂�悤�ɐ���
        if (localAngle.x > maxLimit) {
            localAngle.x = maxLimit;
        }

        if (localAngle.x < minLimit) {
            localAngle.x = minLimit;
        }

        // �J�����̉�]
        transform.localEulerAngles = localAngle;
    }
}
